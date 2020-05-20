using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class PointsService : IPointsService
    {
        private readonly HeroesCupDbContext dbContext;

        public PointsService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int?> getClubPoints(Guid clubId, string schoolYear)
        {
            int points = 0;
            var club = await this.dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);
            if (club == null)
            {
                return null;
            }

            foreach (var mission in club.Missions)
            {
                if (mission.SchoolYear == schoolYear)
                {
                    points += mission.Stars * mission.HeroMissions.Count();
                }
            }

            return points;
        }
    }
}
