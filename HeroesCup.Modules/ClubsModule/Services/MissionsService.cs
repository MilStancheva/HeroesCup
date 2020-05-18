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
    }
}
