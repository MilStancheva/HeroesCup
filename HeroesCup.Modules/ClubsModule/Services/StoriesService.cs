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
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly HeroesCupDbContext dbContext; 

        public StoriesService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
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
                                    Mission = s.Mission,
                                    IsPublished = s.IsPublished
                                })

            };

            return model;
        }

        public Task<StoryEditModel> CreateStoryEditModelAsync(Guid? ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<StoryEditModel> GetStoryEditModelByIdAsync(Guid id, Guid? ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PublishStoryEditModelAsync(Guid storyId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SaveStoryEditModelAsync(StoryEditModel model)
        {
            throw new NotImplementedException();
        }
    }
}
