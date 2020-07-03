using HeroesCup.Data.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IStoryImageService
    {
        Task CreateStoryImageAsync(Image image, Story story);

        Task DeleteStoryImageAsync(StoryImage storyImage, bool commit = false);

        Task<StoryImage> GetStoryImage(Guid storyId);

        string GetStoryImageSource(Story story);
    }
}