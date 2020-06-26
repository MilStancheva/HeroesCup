using HeroesCup.Data.Models;
using System.Collections.Generic;

namespace HeroesCup.Web.Services
{
    public class TimeheroesMissionsService : ITimeheroesMissionsService
    {
        private readonly ClubsModule.Services.Contracts.IMissionIdeasService missionIdeasService;

        public TimeheroesMissionsService(ClubsModule.Services.Contracts.IMissionIdeasService missionIdeasService)
        {
            this.missionIdeasService = missionIdeasService;
        }

        IEnumerable<MissionIdea> ITimeheroesMissionsService.GetMissionIdeas()
        {
            var timeheroesMissions = this.missionIdeasService.GetAllPublishedMissionIdeas();
            return timeheroesMissions;
        }
    }
}
