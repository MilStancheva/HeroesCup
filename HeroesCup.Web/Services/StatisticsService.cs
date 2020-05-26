using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IMissionsService missionsService;
        private readonly IClubsService clubsService;

        public StatisticsService(IMissionsService missionsService, IClubsService clubsService)
        {
            this.missionsService = missionsService;
            this.clubsService = clubsService;
        }

        public int GetAllClubsCount()
        {
            return this.clubsService.GetAllClubs().Count();
        }

        public int GetAllHeroesCount()
        {
            var clubs = this.clubsService.GetAllClubs();
            var heroesCount = 0;
            foreach (var club in clubs)
            {
                heroesCount += club.Heroes.Count();
            }

            return heroesCount;
        }

        public int GetAllHoursCount()
        {
            throw new NotImplementedException();
        }

        public int GetAllMissionsCount()
        {
            return this.missionsService.GetAllHeroesCupPublishedMissions().Count();
        }
    }
}
