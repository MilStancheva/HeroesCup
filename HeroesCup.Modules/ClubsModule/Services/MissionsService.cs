using ClubsModule.Common;
using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                                    HeroesCount = m.HeroMissions != null ? m.HeroMissions.Where(hm => hm.MissionId == m.Id).Count() : 0,
                                    IsPublished = m.IsPublished
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
            var startDate = DateTime.Parse(model.UploadedStartDate);
            var endDate = DateTime.Parse(model.UploadedEndDate);
            mission.StartDate = startDate.ToUnixMilliseconds();
            mission.EndDate = endDate.ToUnixMilliseconds();
            mission.SchoolYear = CalculateSchoolYear(startDate);
            mission.Content = model.Mission.Content;
            mission.TimeheroesUrl = model.Mission.TimeheroesUrl;
            mission.Type = model.Mission.Type;
            mission.IsPublished = false;

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

        private string getEndSchoolYear(int startYear)
        {
            return (startYear + 1).ToString();
        }

        private string getStartSchoolYear(DateTime? startDate)
        {
            var month = startDate.Value.Month;
            if (month >= 8 && month <= 12)
            {
                var startYear = startDate.Value.Year;
                return startYear.ToString();
            }

            if (month >= 1 && month <= 7)
            {
                var startYear = startDate.Value.Year - 1;
                return startYear.ToString();
            }

            return startDate.Value.Year.ToString();
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

        public async Task<MissionEditModel> GetMissionEditModelByIdAsync(Guid id, Guid? ownerId)
        {
            Mission mission = null;
            if (ownerId.HasValue)
            {
                mission = await this.dbContext.Missions
                 .Where(c => c.OwnerId == ownerId.Value)
                 .Include(c => c.HeroMissions)
                 .ThenInclude(m => m.Hero)
                 .Include(c => c.MissionImages)
                 .ThenInclude(ci => ci.Image)
                 .FirstOrDefaultAsync(c => c.Id == id);
            }
            else
            {
                mission = await this.dbContext.Missions
                    .Include(c => c.MissionImages)
                    .ThenInclude(ci => ci.Image)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }

            if (mission == null)
            {
                return null;
            }

            var model = await CreateMissionEditModelAsync(ownerId);
            model.Mission = mission;

            if (mission.MissionImages != null && mission.MissionImages.Count > 0)
            {
                var missionImage = await this.imagesService.GetMissionImage(mission.Id);
                model.ImageSrc = this.imagesService.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);
            }

            model.UploadedStartDate = mission.StartDate.ToUniversalDateTime().ToLocalTime().ToString();
            model.UploadedEndDate = mission.EndDate.ToUniversalDateTime().ToLocalTime().ToString();

            if (mission.HeroMissions != null && mission.HeroMissions.Count > 0)
            {
                foreach (var heroMission in mission.HeroMissions)
                {
                    var hero = await this.dbContext.Heroes.FirstOrDefaultAsync(h => h.Id == heroMission.HeroId);
                    model.Heroes.Add(hero);
                }
            }
            

            return model;
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
    }
}
