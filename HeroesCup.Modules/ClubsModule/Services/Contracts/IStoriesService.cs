using ClubsModule.Models;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IStoriesService
    {
        Task<StoryListModel> GetStoryListModelAsync(Guid? ownerId);

        Task<StoryEditModel> CreateStoryEditModelAsync(Guid? ownerId);

        Task<Guid> SaveStoryEditModelAsync(StoryEditModel model);

        Task<bool> PublishStoryEditModelAsync(Guid storyId);

        Task<bool> UnpublishStoryEditModelAsync(Guid storyId);

        Task<StoryEditModel> GetStoryEditModelByIdAsync(Guid id, Guid? ownerId);

        Task<bool> DeleteAsync(Guid id);

        IEnumerable<Story> GetAllPublishedStories();

        Task<Story> GetStoryByIdAsync(Guid id);

        Task<Story> GetStoryByMissionSlugAsync(String missionSlug);
    }
}