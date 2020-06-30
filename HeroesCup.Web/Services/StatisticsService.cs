using AutoMapper.Configuration.Annotations;
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
        private const int SPENT_HOURS_PER_DAY = 8;
        private readonly ClubsModule.Services.Contracts.IMissionsService missionsService;
        private readonly IClubsService clubsService;

        public StatisticsService(ClubsModule.Services.Contracts.IMissionsService missionsService, IClubsService clubsService)
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
            var missions = this.missionsService.GetAllPublishedMissions();
            var hours = 0;
            foreach (var mission in missions)
            {
                var missionDuration = this.missionsService.GetMissionDuration(mission.StartDate, mission.EndDate);
                var missionHours = ((int)missionDuration.TotalDays * SPENT_HOURS_PER_DAY) * mission.HeroMissions.Count();
                hours += missionHours;
            }

            return hours;
        }

        public int GetAllMissionsCount()
        {
            return this.missionsService.GetAllPublishedMissions().Count();
        }
    }
}