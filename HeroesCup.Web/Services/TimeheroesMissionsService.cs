using HeroesCup.Data.Models;
using System.Collections.Generic;

namespace HeroesCup.Web.Services
{
    public class TimeheroesMissionsService : ITimeheroesMissionsService
    {
        private readonly ClubsModule.Services.Contracts.IMissionsService missionsService;

        public TimeheroesMissionsService(ClubsModule.Services.Contracts.IMissionsService missionsService)
        {
            this.missionsService = missionsService;
        }

        IEnumerable<Mission> ITimeheroesMissionsService.GetTimeheroesMissions()
        {
            var timeheroesMissions = this.missionsService.GetAllTimeheroesPublishedMissions();
            return timeheroesMissions;
        }
    }
}
