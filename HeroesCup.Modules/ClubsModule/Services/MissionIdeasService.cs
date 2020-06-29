using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class MissionIdeasService : IMissionIdeasService
    {
        private readonly HeroesCupDbContext dbContext;
        private readonly IImagesService imagesService;

        public MissionIdeasService(HeroesCupDbContext dbContext, IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.imagesService = imagesService;
        }

        public async Task<MissionIdeaListModel> GetMissionIdeasListModelAsync()
        {
            var missionIdeas = new List<MissionIdea>();
            missionIdeas = await this.dbContext.MissionIdeas.ToListAsync();

            var model = new MissionIdeaListModel()
            {
                MissionIdeas = missionIdeas.OrderBy(m => m.IsPublished)
                                .Select(m => new MissionIdeaListItem()
                                {
                                    Id = m.Id,
                                    Title = m.Title,
                                    IsPublished = m.IsPublished
                                })

            };

            return model;
        }

        public MissionIdeaEditModel CreateMissionIdeaEditModel()
        {
            var model = new MissionIdeaEditModel()
            {
                MissionIdea = new MissionIdea(),
            };

            return model;
        }

        public IEnumerable<MissionIdea> GetAllPublishedMissionIdeas()
        {
            var missionIdeas = this.dbContext.MissionIdeas
                .Where(m => m.IsPublished == true)
                .Include(m => m.MissionIdeaImages)
                .ThenInclude(mi => mi.Image);

            return missionIdeas;
        }

        public async Task<MissionIdeaEditModel> GetMissionIdeaEditModelByIdAsync(Guid id)
        {
            MissionIdea missionIdea = null;
            missionIdea = await this.dbContext.MissionIdeas
                    .Include(c => c.MissionIdeaImages)
                    .ThenInclude(ci => ci.Image)
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (missionIdea == null)
            {
                return null;
            }

            var model = CreateMissionIdeaEditModel();
            model.MissionIdea = missionIdea;

            if (missionIdea.MissionIdeaImages != null && missionIdea.MissionIdeaImages.Count > 0)
            {
                var missionIdeaImage = await this.imagesService.GetMissionIdeaImageAsync(missionIdea.Id);
                model.ImageSrc = this.imagesService.GetImageSource(missionIdeaImage.Image.ContentType, missionIdeaImage.Image.Bytes);
            }

            return model;
        }

        public async Task<Guid> SaveMissionIdeaEditModelAsync(MissionIdeaEditModel model)
        {
            var missionIdea = await this.dbContext.MissionIdeas
                .Include(c => c.MissionIdeaImages)
                .ThenInclude(m => m.Image)
                .FirstOrDefaultAsync(m => m.Id == model.MissionIdea.Id);

            if (missionIdea == null)
            {
                missionIdea = new MissionIdea();
                missionIdea.Id = model.MissionIdea.Id != Guid.Empty ? model.MissionIdea.Id : Guid.NewGuid();
                this.dbContext.MissionIdeas.Add(missionIdea);
            }

            missionIdea.Title = model.MissionIdea.Title;
            missionIdea.Content = model.MissionIdea.Content;
            missionIdea.TimeheroesUrl = model.MissionIdea.TimeheroesUrl;

            // set missionIdea image
            if (model.Image != null)
            {
                var image = new Image();
                var bytes = this.imagesService.GetByteArrayFromImage(model.Image);
                var filename = this.imagesService.GetFilename(model.Image);
                var contentType = this.imagesService.GetFileContentType(model.Image);
                image.Bytes = bytes;
                image.Filename = filename;
                image.ContentType = contentType;

                await this.imagesService.CreateMissionIdeaImageAsync(image, missionIdea);
            }

            await dbContext.SaveChangesAsync();
            return missionIdea.Id;
        }

        public async Task<bool> DeleteMissionIdeaAsync(Guid id)
        {
            var missionIdea = this.dbContext.MissionIdeas.FirstOrDefault(c => c.Id == id);
            if (missionIdea == null)
            {
                return false;
            }

            this.dbContext.MissionIdeas.Remove(missionIdea);
            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PublishMissionIdeaAsync(Guid missionIdeaId)
        {
            var missionIdea = await this.dbContext.MissionIdeas.FirstOrDefaultAsync(m => m.Id == missionIdeaId);
            if (missionIdea == null)
            {
                return false;
            }

            missionIdea.IsPublished = true;
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnpublishMissionIdeaAsync(Guid missionIdeaId)
        {
            var missionIdea = await this.dbContext.MissionIdeas.FirstOrDefaultAsync(m => m.Id == missionIdeaId);
            if (missionIdea == null)
            {
                return false;
            }

            missionIdea.IsPublished = false;
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
