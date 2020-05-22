using ClubsModule.Models;
using System;
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
    }
}
