using HeroesCup.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public interface IMissionsService
    {
        IEnumerable<MissionIdeaViewModel> GetMissionIdeaViewModels();

        IEnumerable<MissionViewModel> GetMissionViewModels();

        Task<IEnumerable<MissionViewModel>> GetPinnedMissionViewModels();

        IDictionary<string, int> GetMissionsPerLocation();

        int GetAllMissionsCount();

        IEnumerable<MissionViewModel> GetMissionViewModelsByLocation(string location);

        Task<MissionViewModel> GetMissionViewModelByIdAsync(Guid id);

        Task<MissionViewModel> GetMissionViewModelBySlugAsync(String slug);

        Task<MissionIdeaViewModel> GetMissionIdeaViewModelByIdAsync(Guid id);

        IEnumerable<StoryViewModel> GetAllPublishedStoryViewModels();

        Task<StoryViewModel> GetStoryViewModelByIdAsync(Guid id);

        Task<StoryViewModel> GetStoryViewModelByMissionSlugAsync(String missionSlug);
    }
}
