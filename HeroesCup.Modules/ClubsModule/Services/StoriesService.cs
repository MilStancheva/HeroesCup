using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using Piranha.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly HeroesCupDbContext dbContext;
        private readonly IImagesService imagesService;

        public StoriesService(HeroesCupDbContext dbContext, IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.imagesService = imagesService;
        }

        public async Task<StoryListModel> GetStoryListModelAsync(Guid? ownerId)
        {
            var stories = new List<Story>();
            stories = await this.dbContext.Stories
                    .Include(c => c.Mission)
                    .ThenInclude(m => m.Club)
                    .ToListAsync();

            if (ownerId.HasValue)
            {
                stories = stories.Where(c => c.Mission.OwnerId == ownerId.Value).ToList();
            }

            var model = new StoryListModel()
            {
                Stories = stories.OrderBy(s => s.IsPublished)
                                .Select(s => new StoryListItem()
                                {
                                    Id = s.Id,
                                    StartText = GetShortIntroText(s.Content, 50),
                                    Mission = s.Mission,
                                    IsPublished = s.IsPublished
                                })

            };

            return model;
        }

        public async Task<StoryEditModel> CreateStoryEditModelAsync(Guid? ownerId)
        {
            var missions = new List<Mission>();
            if (ownerId.HasValue)
            {
                missions = await this.dbContext.Missions.Where(m => m.OwnerId == ownerId.Value).ToListAsync();
            }
            else
            {
                missions = await this.dbContext.Missions.ToListAsync();
            }

            var model = new StoryEditModel()
            {
                Story = new Story(),
                Missions = missions != null ? missions : new List<Mission>()
            };

            return model;
        }

        public async Task<StoryEditModel> GetStoryEditModelByIdAsync(Guid id, Guid? ownerId)
        {
            var story = await this.dbContext.Stories
                   .Include(s => s.Mission)
                   .ThenInclude(m => m.Club)
                   .Include(c => c.StoryImages)
                   .ThenInclude(ci => ci.Image)
                   .FirstOrDefaultAsync(c => c.Id == id);

            if (story == null)
            {
                return null;
            }

            if (ownerId.HasValue && story.Mission.Club.OwnerId != ownerId)
            {
                return null;
            }

            var model = await CreateStoryEditModelAsync(ownerId);
            model.Story = story;

            if (story.StoryImages != null && story.StoryImages.Count > 0)
            {
                var storyImages = await this.imagesService.GetStoryImages(story.Id);
                if (model.ImageSources == null)
                {
                    model.ImageSources = new List<string>();
                }
                foreach (var storyImage in storyImages)
                {
                    model.ImageSources.Add(this.imagesService.GetImageSource(storyImage.Image.ContentType, storyImage.Image.Bytes));
                }
            }

            return model;
        }

        public async Task<bool> PublishStoryEditModelAsync(Guid storyId)
        {
            var story = await this.dbContext.Stories.FirstOrDefaultAsync(m => m.Id == storyId);
            if (story == null)
            {
                return false;
            }

            story.IsPublished = true;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnpublishStoryEditModelAsync(Guid storyId)
        {
            var story = await this.dbContext.Stories.FirstOrDefaultAsync(m => m.Id == storyId);
            if (story == null)
            {
                return false;
            }

            story.IsPublished = false;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Guid> SaveStoryEditModelAsync(StoryEditModel model)
        {
            var story = await this.dbContext.Stories
                .Include(c => c.Mission)
                .Include(c => c.StoryImages)
                .FirstOrDefaultAsync(h => h.Id == model.Story.Id);

            if (story == null)
            {
                story = new Story();
                story.Id = model.Story.Id != Guid.Empty ? model.Story.Id : Guid.NewGuid();
                this.dbContext.Stories.Add(story);
            }

            if (model.Story.MissionId != null && model.Story.MissionId != Guid.Empty)
            {
                story.Mission = this.dbContext.Missions.FirstOrDefault(m => m.Id == model.Story.MissionId);
            }

            story.Content = model.Story.Content;

            // set story image
            if (model.UploadedImages != null)
            {
                var images = new List<Image>();
                foreach (var uploadedImage in model.UploadedImages)
                {
                    var image = new Image();
                    var bytes = this.imagesService.GetByteArrayFromImage(uploadedImage);
                    var filename = this.imagesService.GetFilename(uploadedImage);
                    var contentType = this.imagesService.GetFileContentType(uploadedImage);
                    image.Bytes = bytes;
                    image.Filename = filename;
                    image.ContentType = contentType;

                    images.Add(image);
                }

                await this.imagesService.CreateStoryImagesAsync(images, story);
            }

            await dbContext.SaveChangesAsync();
            return story.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var story = this.dbContext.Stories.FirstOrDefault(c => c.Id == id);
            if (story == null)
            {
                return false;
            }

            this.dbContext.Stories.Remove(story);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        private string GetShortIntroText(string htmlString, int length)
        {
            var text = GetPlainTextFromHtmlString(htmlString);
            text = GetShortTextFromString(text, length);

            return text + "...";
        }

        private string GetPlainTextFromHtmlString(string htmlString)
        {
            return Regex.Replace(htmlString, @"<(.|\n)*?>", "");
        }

        private string GetShortTextFromString(string htmlString, int length)
        {
            return htmlString.Substring(0, Math.Min(htmlString.Length, length));
        }
    }
}