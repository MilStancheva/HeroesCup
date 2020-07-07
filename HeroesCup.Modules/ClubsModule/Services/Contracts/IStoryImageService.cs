using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IStoryImageService
    {
        Task CreateStoryImagesAsync(IEnumerable<Image> images, Story story);

        Task DeleteStoryImageAsync(StoryImage storyImage, bool commit = false);

        Task<IEnumerable<StoryImage>> GetStoryImages(Guid storyId);

        IEnumerable<string> GetStoryImageSources(Story story);
    }
}