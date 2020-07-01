using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public interface IMissionsService
    {
        IEnumerable<MissionIdea> GetMissionIdeas();

        IEnumerable<MissionIdeaViewModel> GetMissionIdeaViewModels();

        IEnumerable<MissionViewModel> GetMissionViewModels();

        Task<IEnumerable<MissionViewModel>> GetPinnedMissions();

        IDictionary<string, int> GetMissionsPerLocation();

        int GetAllMissionsCount();

       IEnumerable<MissionViewModel> GetMissionsByLocation(string location);

        Task<MissionViewModel> GetMissionViewModelByIdAsync(Guid id);
    }
}
