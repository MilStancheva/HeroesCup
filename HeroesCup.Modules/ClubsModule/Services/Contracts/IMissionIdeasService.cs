using ClubsModule.Models;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IMissionIdeasService
    {
        Task<MissionIdeaListModel> GetMissionIdeasListModelAsync();

        IEnumerable<MissionIdea> GetAllPublishedMissionIdeas();

        MissionIdeaEditModel CreateMissionIdeaEditModel();

        Task<MissionIdeaEditModel> GetMissionIdeaEditModelByIdAsync(Guid id);

        Task<Guid> SaveMissionIdeaEditModelAsync(MissionIdeaEditModel model);
    }
}
