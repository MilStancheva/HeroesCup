using ClubsModule.Common;
using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
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

        public MissionsService(HeroesCupDbContext dbContext, IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.imagesService = imagesService;
        }

        public async Task<MissionListModel> GetMissionListModelAsync(Guid? ownerId)
        {
            var missions = new List<Mission>();
            if (ownerId.HasValue)
            {
                missions = await this.dbContext.Missions
                    .Where(m => m.Club.OwnerId == ownerId.Value)
                    .Include(h => h.Club)
                    .ToListAsync();
            }
            else
            {
                missions = await this.dbContext.Missions
                    .Include(m => m.Club)
                    .ToListAsync();
            }

            if (missions == null)
            {
                missions = new List<Mission>();
            }

            var model = new MissionListModel()
            {
                Missions = missions.OrderByDescending(m => m.StartDate)
                                .Select(m => new MissionListItem()
                                {
                                    Id = m.Id,
                                    Title = m.Title,
                                    ClubId = m.ClubId,
                                    ClubName = m.Club.Name,
                                    HeroesCount = m.HeroMissions != null ? m.HeroMissions.Where(hm => hm.MissionId == m.Id).Count() : 0
                                })

            };

            return model;
        }

        public async Task<MissionEditModel> CreateMissionEditModelAsync(Guid? ownerId)
        {
            var clubs = new List<Club>();
            if (ownerId.HasValue)
            {
                clubs = await this.dbContext.Clubs
                    .Where(c => c.OwnerId == ownerId.Value).ToListAsync();
            }
            else
            {
                clubs = await this.dbContext.Clubs.ToListAsync();
            }

            var model = new MissionEditModel()
            {
                Mission = new Mission(),
                Heroes = new List<Hero>(),
                Clubs = clubs,
                MissionTypes = new MissionType[]
                {
                    MissionType.TimeheroesMission,
                    MissionType.HeroesCupMission
                }
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
                mission.OwnerId = model.Mission.OwnerId;
                this.dbContext.Missions.Add(mission);
            }

            mission.Title = model.Mission.Title;
            mission.Location = model.Mission.Location;
            mission.Stars = model.Mission.Stars;
            var startDate = DateTime.ParseExact(model.UploadedStartDate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(model.UploadedEndDate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            mission.StartDate = startDate.ToUnixMilliseconds();
            mission.EndDate = endDate.ToUnixMilliseconds();
            mission.SchoolYear = CalculateSchoolYear(startDate);
            mission.Content = model.Mission.Content;
            mission.TimeheroesUrl = model.Mission.TimeheroesUrl;
            mission.Type = model.Mission.Type;
            mission.isPublished = false;

            // set missions's heroes
            if (model.HeroesIds != null && model.HeroesIds.Any())
            {
                var heroMissions = new List<HeroMission>();
                foreach (var heroId in model.HeroesIds)
                {
                    var hero = this.dbContext.Heroes.FirstOrDefault(h => h.Id == heroId);

                    heroMissions.Add(new HeroMission() { 
                        Hero = hero,
                        Mission = mission
                    });
                }
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

        private string CalculateSchoolYear(DateTime startDate)
        {
            var startYear = getStartSchoolYear(startDate);
            var endYear = getEndSchoolYear(int.Parse(startYear));

            return $"{startYear} / {endYear}";
        }

        private String getEndSchoolYear(int value)
        {
            return (value + 1).ToString();
        }

        private String getStartSchoolYear(DateTime? value)
        {
            var month = value.Value.Month;
            if (month >= 8 && month <= 12)
            {
                var startYear = value.Value.Year;
                return startYear.ToString();
            }

            if (month >= 1 && month <= 7)
            {
                var startYear = value.Value.Year - 1;
                return startYear.ToString();
            }

            return value.Value.Year.ToString();
        }

        public async Task<bool> PublishMissionEditModelAsync(Guid missionId)
        {
            var mission = await this.dbContext.Missions.FirstOrDefaultAsync(m => m.Id == missionId);
            if (mission == null)
            {
                return false;
            }

            mission.isPublished = true;
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
