using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
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
    }
}
