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

        public MissionsService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                Clubs = clubs
        };

            model.Mission.OwnerId = ownerId.HasValue ? ownerId.Value : Guid.Empty;
            return model;
        }

        public async Task<Guid> SaveMissionEditModelAsync(MissionEditModel model)
        {
            var mission = await this.dbContext.Missions
                .Include(c => c.Image)
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
            //mission.StartDate = model.UploadedStartDate
            //mission.EndDate = model.UploadedEndDate
            mission.Content = model.Mission.Content;
            mission.Club = model.Mission.Club;
            mission.SchoolYear = this.CalculateSchoolYear(mission.StartDate);

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

            // set club logo
            //if (model.Image != null)
            //{
            //    var image = new Image();
            //    var bytes = GetByteArrayFromImage(model.UploadedLogo);
            //    var filename = Path.GetFileName(model.Image.FileName);
            //    var contentType = model.Image.ContentType;
            //    image.Bytes = bytes;
            //    image.Filename = filename;
            //    image.ContentType = contentType;
            //    image.Club = club;

            //    await this.imagesService.Create(image);
            //}

            await dbContext.SaveChangesAsync();
            return mission.Id;
        }

        private int CalculateSchoolYear(long startDate)
        {
            throw new NotImplementedException();
        }
    }
}
