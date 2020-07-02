﻿using ClubsModule.Common;
using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class MissionsService : IMissionsService
    {
        private readonly HeroesCupDbContext dbContext;
        private readonly IImagesService imagesService;
        private readonly ISchoolYearService schoolYearService;
        private readonly IConfiguration configuration;

        public MissionsService(HeroesCupDbContext dbContext,
            IImagesService imagesService,
            ISchoolYearService schoolYearService,
            IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.imagesService = imagesService;
            this.schoolYearService = schoolYearService;
            this.configuration = configuration;
        }

        public async Task<MissionListModel> GetMissionListModelAsync(Guid? ownerId)
        {
            var missions = new List<Mission>();
            missions = await this.dbContext.Missions
                    .Include(m => m.Club)
                    .Include(m => m.HeroMissions)
                    .ThenInclude(hm => hm.Hero)
                    .ToListAsync();

            if (ownerId.HasValue)
            {
                missions = missions.Where(m => m.Club.OwnerId == ownerId.Value).ToList();
            }

            var model = new MissionListModel()
            {
                Missions = missions.OrderBy(m => m.IsPublished)
                                .Select(m => new MissionListItem()
                                {
                                    Id = m.Id,
                                    Title = m.Title,
                                    ClubId = m.ClubId,
                                    ClubName = m.Club.Name,
                                    HeroesCount = m.HeroMissions != null ? m.HeroMissions.Where(hm => hm.MissionId == m.Id).Count() : 0,
                                    IsPublished = m.IsPublished,
                                    IsPinned = m.IsPinned
                                })

            };

            return model;
        }

        public async Task<MissionEditModel> CreateMissionEditModelAsync(Guid? ownerId)
        {
            var clubs = new List<Club>();
            clubs = await this.dbContext.Clubs.ToListAsync();
            ICollection<Hero> heroes = new List<Hero>();

            if (ownerId.HasValue)
            {
                clubs = clubs.Where(c => c.OwnerId == ownerId.Value).ToList();
                heroes = await GetHeroes(null, ownerId);
            }

            var model = new MissionEditModel()
            {
                Mission = new Mission(),
                Heroes = heroes,
                HeroesIds = new List<Guid>(),
                Clubs = clubs,
                ClubId = clubs.Count > 0 ? clubs.FirstOrDefault().Id : Guid.NewGuid()
            };

            model.Mission.OwnerId = ownerId.HasValue ? ownerId.Value : Guid.Empty;
            return model;
        }

        public async Task<Guid> SaveMissionEditModelAsync(MissionEditModel model)
        {
            var mission = await this.dbContext.Missions
                .Include(c => c.MissionImages)
                .ThenInclude(m => m.Image)
                .Include(m => m.Club)
                .FirstOrDefaultAsync(m => m.Id == model.Mission.Id && m.OwnerId == model.Mission.OwnerId);

            if (mission == null)
            {
                mission = new Mission();
                mission.Id = model.Mission.Id != Guid.Empty ? model.Mission.Id : Guid.NewGuid();
                var club = await this.dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == model.ClubId);
                mission.OwnerId = club.OwnerId;
                this.dbContext.Missions.Add(mission);
            }

            mission.Title = model.Mission.Title;
            mission.Location = model.Mission.Location;
            if (model.Mission.Stars != 0)
            {
                mission.Stars = model.Mission.Stars;
            }

            var dateFormat = this.configuration["DateFormat"];
            var startDate = DateTime.ParseExact(model.UploadedStartDate, dateFormat, CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(model.UploadedEndDate, dateFormat, CultureInfo.InvariantCulture);
            mission.StartDate = startDate.ToUnixMilliseconds();
            mission.EndDate = endDate.ToUnixMilliseconds();
            mission.SchoolYear = this.schoolYearService.CalculateSchoolYear(startDate);
            mission.Content = model.Mission.Content;

            // set missions's heroes
            if (model.HeroesIds != null && model.HeroesIds.Any())
            {
                await DeleteHeroMissions(mission);
                await AddHeroesToMission(mission, model.HeroesIds, false);
            }

            // set mission organizer
            if (model.ClubId != null && model.ClubId != Guid.Empty)
            {
                var newOrganizator = this.dbContext.Clubs.FirstOrDefault(h => h.Id == model.ClubId);
                mission.Club = newOrganizator;
            }

            // set mission image
            if (model.Image != null)
            {
                var image = new Image();
                var bytes = this.imagesService.GetByteArrayFromImage(model.Image);
                var filename = this.imagesService.GetFilename(model.Image);
                var contentType = this.imagesService.GetFileContentType(model.Image);
                image.Bytes = bytes;
                image.Filename = filename;
                image.ContentType = contentType;

                await this.imagesService.CreateMissionImageAsync(image, mission);
            }

            await dbContext.SaveChangesAsync();
            return mission.Id;
        }

        public async Task<bool> PublishMissionEditModelAsync(Guid missionId)
        {
            var mission = await this.dbContext.Missions.FirstOrDefaultAsync(m => m.Id == missionId);
            if (mission == null)
            {
                return false;
            }

            mission.IsPublished = true;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnpublishMissionEditModelAsync(Guid missionId)
        {
            var mission = await this.dbContext.Missions.FirstOrDefaultAsync(m => m.Id == missionId);
            if (mission == null)
            {
                return false;
            }

            mission.IsPublished = false;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<MissionEditModel> GetMissionEditModelByIdAsync(Guid id, Guid? ownerId)
        {
            Mission mission = null;
            mission = await this.dbContext.Missions
                    .Include(m => m.Club)
                    .Include(c => c.HeroMissions)
                    .ThenInclude(m => m.Hero)
                    .Include(c => c.MissionImages)
                    .ThenInclude(ci => ci.Image)
                    .Include(m => m.Story)
                    .ThenInclude(s => s.StoryImages)
                    .ThenInclude(s => s.Image)
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (mission == null)
            {
                return null;
            }

            var model = await CreateMissionEditModelAsync(ownerId);
            model.Mission = mission;
            model.ClubId = mission.Club.Id;
            model.Heroes = await GetHeroes(mission.Club.Id, ownerId);

            if (mission.MissionImages != null && mission.MissionImages.Count > 0)
            {
                var missionImage = await this.imagesService.GetMissionImage(mission.Id);
                model.ImageSrc = this.imagesService.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);
            }

            var dateFormat = this.configuration["DateFormat"];
            model.UploadedStartDate = mission.StartDate.ToUniversalDateTime().ToLocalTime().ToString(dateFormat);
            model.UploadedEndDate = mission.EndDate.ToUniversalDateTime().ToLocalTime().ToString(dateFormat);
            model.Duration = GetMissionDuration(mission.StartDate, mission.EndDate);

            if (mission.HeroMissions != null && mission.HeroMissions.Count > 0)
            {
                foreach (var heroMission in mission.HeroMissions)
                {
                    var hero = await this.dbContext.Heroes.FirstOrDefaultAsync(h => h.Id == heroMission.HeroId);
                    model.HeroesIds.Add(hero.Id);
                }
            }


            return model;
        }

        private async Task<ICollection<Hero>> GetHeroes(Guid? clubId, Guid? ownerId)
        {
            var heroes = this.dbContext.Heroes
                     .Include(h => h.Club)
                     .Select(x => x);                     

            if (clubId.HasValue)
            {
                heroes = heroes.Where(h => h.ClubId == clubId);
            }

            if (ownerId.HasValue)
            {
                heroes = heroes.Where(h => h.Club.OwnerId == ownerId.Value);
            }

            return await heroes.ToListAsync();
        }

        public TimeSpan GetMissionDuration(long startDate, long endDate)
        {
            return endDate.ToUniversalDateTime() - startDate.ToUniversalDateTime();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var mission = this.dbContext.Missions.FirstOrDefault(c => c.Id == id);
            if (mission == null)
            {
                return false;
            }

            this.dbContext.Missions.Remove(mission);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        private async Task DeleteHeroMissions(Mission mission, bool commit = false)
        {
            var heroMissions = this.dbContext.HeroMissions.Where(hm => hm.MissionId == mission.Id);
            foreach (var heroMission in heroMissions)
            {
                this.dbContext.HeroMissions.Remove(heroMission);
            }

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }

        private async Task AddHeroesToMission(Mission mission, IEnumerable<Guid> heroesIds, bool commit = false)
        {
            var heroMissions = new List<HeroMission>();
            foreach (var heroId in heroesIds)
            {
                var hero = this.dbContext.Heroes.FirstOrDefault(h => h.Id == heroId);
                heroMissions.Add(new HeroMission()
                {
                    Hero = hero,
                    Mission = mission
                });
            }

            mission.HeroMissions = heroMissions;

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Mission>> GetMissionsBySchoolYear(string schoolYear)
        {
            var now = DateTime.UtcNow.ToUnixMilliseconds();
            return await this.dbContext.Missions
                .Where(m => m.IsPublished)
                .Include(c => c.Club)
                .ThenInclude(c => c.Missions)
                .ThenInclude(m => m.MissionImages)
                .Include(m => m.Club)
                .ThenInclude(c => c.ClubImages)
                .ThenInclude(ci => ci.Image)
                .Include(m => m.MissionImages)
                .Include(m => m.HeroMissions)
                .ThenInclude(hm => hm.Hero)
                .Where(m => m.SchoolYear == schoolYear)
                .Where(m => now > m.EndDate && m.Stars != 0 && m.HeroMissions != null && m.HeroMissions.Count > 0)
                .OrderByDescending(c => c.StartDate)
                .ToListAsync();
        }

        public IEnumerable<string> GetMissionSchoolYears()
        {
            var schoolYears = this.dbContext.Missions
               .Where(m => m.IsPublished)
               .GroupBy(m => m.SchoolYear)
               .Select(sy => sy.Key);

            return schoolYears.ToArray();
        }

        public IEnumerable<Mission> GetAllPublishedMissions()
        {
            var missions = this.dbContext.Missions
                .Where(m => m.IsPublished == true)
                .Include(m => m.HeroMissions)
                .Include(m => m.Club)
                .Include(m => m.MissionImages)
                .ThenInclude(mi => mi.Image)
                .Include(m => m.Story)
                .ThenInclude(m => m.StoryImages)
                .ThenInclude(m => m.Image);

            return missions;
        }

        public async Task<bool> PinMissionEditModelAsync(Guid id)
        {
            var mission = await this.dbContext.Missions.FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null)
            {
                return false;
            }

            mission.IsPinned = true;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnpinMissionEditModelAsync(Guid id)
        {
            var mission = await this.dbContext.Missions.FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null)
            {
                return false;
            }

            mission.IsPinned = false;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Mission>> GetPinnedMissions()
        {
            var missions = await this.dbContext.Missions
                .Where(m => m.IsPinned == true && m.IsPublished == true)
                .Include(m => m.Club)
                .Include(m => m.MissionImages)
                .ThenInclude(mi => mi.Image)
                .Include(m => m.Story)
                .ThenInclude(s => s.StoryImages)
                .ThenInclude(si => si.Image)
                .ToListAsync();

            int countOfPinnedMissionsOnHomePage;
            int.TryParse(this.configuration["PinnedMissionsOnHomePageCount"], out countOfPinnedMissionsOnHomePage);

            if (missions.Count() < countOfPinnedMissionsOnHomePage)
            {
                var countOfMissionsToAdd = countOfPinnedMissionsOnHomePage - missions.Count();
                var latestMissions = await this.dbContext.Missions
                    .Where(m => m.IsPublished)
                    .OrderByDescending(m => m.StartDate)
                    .Take(countOfMissionsToAdd)
                    .Include(m => m.Club)
                    .Include(m => m.MissionImages)
                    .ThenInclude(mi => mi.Image)
                    .Include(m => m.Story)
                    .ThenInclude(s => s.StoryImages)
                    .ThenInclude(si => si.Image)
                    .ToListAsync();

                missions.AddRange(latestMissions);
            }

            return missions.Take(countOfPinnedMissionsOnHomePage);
        }
    }
}