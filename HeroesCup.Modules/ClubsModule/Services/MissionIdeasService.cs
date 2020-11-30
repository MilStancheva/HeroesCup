using ClubsModule.Common;
using ClubsModule.Exceptions;
using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class MissionIdeasService : IMissionIdeasService
    {
        private readonly HeroesCupDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly IImagesService imagesService;

        private string dateTimeFormat;

        public MissionIdeasService(HeroesCupDbContext dbContext, IImagesService imagesService, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.imagesService = imagesService;
            this.configuration = configuration;
            this.dateTimeFormat = this.configuration["DateТimeFormat"];
        }

        public async Task<MissionIdeaListModel> GetMissionIdeasListModelAsync()
        {
            var missionIdeas = new List<MissionIdea>();
            missionIdeas = await this.dbContext.MissionIdeas.ToListAsync();

            var model = new MissionIdeaListModel()
            {
                MissionIdeas = missionIdeas
                                .OrderBy(m => m.IsPublished)
                                .ThenByDescending(m => m.UpdatedOn)
                                .Select(m => new MissionIdeaListItem()
                                {
                                    Id = m.Id,
                                    Title = m.Title,
                                    IsPublished = m.IsPublished,
                                    LastUpdateOn = m.UpdatedOn.ToUniversalDateTime().ToLocalTime().ToString(this.dateTimeFormat)
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
                .OrderByDescending(mi => mi.StartDate != long.MinValue ? mi.StartDate : mi.CreatedOn);

            return missionIdeas;
        }

        public async Task<MissionIdeaEditModel> GetMissionIdeaEditModelByIdAsync(Guid id)
        {
            MissionIdea missionIdea = null;
            missionIdea = await this.dbContext.MissionIdeas
                    .Include(c => c.MissionIdeaImages)
                    .ThenInclude(ci => ci.Image)
                    .FirstOrDefaultAsync(c => c.Id == id);

            return await MapMissionIdeaToMissionIdeaEditModel(missionIdea);
        }

        public async Task<MissionIdeaEditModel> GetMissionIdeaEditModelBySlugAsync(string slug)
        {
            MissionIdea missionIdea = null;
            missionIdea = await this.dbContext.MissionIdeas
                    .Include(c => c.MissionIdeaImages)
                    .ThenInclude(ci => ci.Image)
                    .FirstOrDefaultAsync(c => c.Slug == slug);

            return await MapMissionIdeaToMissionIdeaEditModel(missionIdea);
        }

        public async Task<Guid> SaveMissionIdeaEditModelAsync(MissionIdeaEditModel model)
        {
            var missionIdea = await this.dbContext.MissionIdeas
                .Include(c => c.MissionIdeaImages)
                .ThenInclude(m => m.Image)
                .FirstOrDefaultAsync(m => m.Id == model.MissionIdea.Id);

            var slug = model.MissionIdea.Title.Trim().ToSlug();
            slug = slug.Unidecode();

            var missionIdeaWithSameTitle = await this.dbContext.MissionIdeas
                .Where(m => (m.Title == model.MissionIdea.Title || m.Slug == slug) && 
                m.Id != model.MissionIdea.Id)
                .FirstOrDefaultAsync();

            if (missionIdeaWithSameTitle != null)
            {
                throw new ExistingItemException();
            }

            if (missionIdea == null)
            {
                missionIdea = new MissionIdea();
                missionIdea.Id = model.MissionIdea.Id != Guid.Empty ? model.MissionIdea.Id : Guid.NewGuid();
                missionIdea.CreatedOn = DateTime.Now.ToUnixMilliseconds();
                this.dbContext.MissionIdeas.Add(missionIdea);
            }

            missionIdea.Title = model.MissionIdea.Title;
            missionIdea.Slug = slug;
            missionIdea.Organization = model.MissionIdea.Organization;
            missionIdea.Location = model.MissionIdea.Location;
            missionIdea.Content = model.MissionIdea.Content;
            missionIdea.TimeheroesUrl = model.MissionIdea.TimeheroesUrl;
            var dateFormat = this.configuration["DateFormat"];
            var startDate = DateTime.ParseExact(model.UploadedStartDate, dateFormat, CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(model.UploadedEndDate, dateFormat, CultureInfo.InvariantCulture);
            missionIdea.StartDate = startDate.StartOfTheDay().ToUnixMilliseconds();
            missionIdea.EndDate = endDate.EndOfTheDay().ToUnixMilliseconds();

            // set missionIdea image
            if (model.Image != null)
            {
                var image = this.imagesService.MapFormFileToImage(model.Image);
                await this.imagesService.CreateMissionIdeaImageAsync(image, missionIdea);
            }

            missionIdea.UpdatedOn = DateTime.Now.ToUnixMilliseconds();

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

        private async Task<MissionIdeaEditModel> MapMissionIdeaToMissionIdeaEditModel(MissionIdea missionIdea)
        {
            if (missionIdea == null)
            {
                return null;
            }

            var model = CreateMissionIdeaEditModel();
            model.MissionIdea = missionIdea;
            var dateFormat = this.configuration["DateFormat"];
            model.UploadedStartDate = missionIdea.StartDate.ToUniversalDateTime().ToLocalTime().ToString(dateFormat);
            model.UploadedEndDate = missionIdea.EndDate.ToUniversalDateTime().ToLocalTime().ToString(dateFormat);

            if (missionIdea.MissionIdeaImages != null && missionIdea.MissionIdeaImages.Count > 0)
            {
                var missionIdeaImage = await this.imagesService.GetMissionIdeaImageAsync(missionIdea.Id);
                model.ImageId = missionIdea.MissionIdeaImages != null && missionIdea.MissionIdeaImages.Any() ? missionIdea.MissionIdeaImages.FirstOrDefault().ImageId.ToString() : null;
                model.ImageFilename = missionIdeaImage.Image.Filename;
            }

            return model;
        }
    }
}
