using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class ImagesService : IImagesService
    {
        private readonly HeroesCupDbContext dbContext;

        public ImagesService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateClubImageAsync(Image image, Club club)
        {
            var oldClubImages = this.dbContext.ClubImages.Where(ci => ci.ClubId == club.Id)
                .Include(ci => ci.Image);

            if (oldClubImages != null)
            {
                foreach (var clubImage in oldClubImages)
                {
                    await this.DeleteClubImageAsync(clubImage);
                }
            }

            this.dbContext.Images.Add(image);
            this.dbContext.ClubImages.Add(new ClubImage()
            {
                Club = club,
                Image = image
            });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteClubImageAsync(ClubImage clubImage, bool commit = false)
        {
            this.dbContext.Images.Remove(clubImage.Image);

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<ClubImage> GetClubImage(Guid clubId)
        {
            return await this.dbContext.ClubImages.Where(ci => ci.ClubId == clubId).FirstOrDefaultAsync();
        }

        public async Task<Image> GetImage(Guid id)
        {
            return await this.dbContext.Images.FirstOrDefaultAsync(i => i.Id == id);
        }

        public string GetImageSource(string contentType, byte[] bytes)
        {
            var bytesTobase64 = Convert.ToBase64String(bytes);
            return string.Format("data:{0};base64,{1}", contentType, bytesTobase64);
        }

        public byte[] GetByteArrayFromImage(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                return target.ToArray();
            }
        }

        public string GetFilename(IFormFile file)
        {
            return Path.GetFileName(file.FileName);
        }

        public string GetFileContentType(IFormFile file)
        {
            return file.ContentType;
        }

        public async Task CreateMissionImageAsync(Image image, Mission mission)
        {
            var oldMissionImages = this.dbContext.MissionImages.Where(mi => mi.MissionId == mission.Id)
                .Include(mi => mi.Image);

            if (oldMissionImages != null)
            {
                foreach (var missionImage in oldMissionImages)
                {
                    await this.DeleteMissionImageAsync(missionImage);
                }
            }

            this.dbContext.Images.Add(image);
            this.dbContext.MissionImages.Add(new MissionImage()
            {
                Mission = mission,
                Image = image
            });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteMissionImageAsync(MissionImage missionImage, bool commit = false)
        {
            this.dbContext.Images.Remove(missionImage.Image);

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<MissionImage> GetMissionImage(Guid missionId)
        {
            return await this.dbContext.MissionImages
                .Where(mi => mi.MissionId == missionId)
                .Include(mi => mi.Image)
                .FirstOrDefaultAsync();
        }

        public async Task CreateStoryImagesAsync(IEnumerable<Image> images, Story story)
        {
            var oldStoryImages = this.dbContext.StoryImages.Where(si => si.StoryId == story.Id)
                .Include(si => si.Image);

            if (oldStoryImages != null)
            {
                foreach (var storyImage in oldStoryImages)
                {
                    await this.DeleteStoryImageAsync(storyImage);
                }
            }

            foreach (var image in images)
            {
                this.dbContext.Images.Add(image);
                this.dbContext.StoryImages.Add(new StoryImage()
                {
                    Story = story,
                    Image = image
                });
            }            

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteStoryImageAsync(StoryImage storyImage, bool commit = false)
        {
            this.dbContext.Images.Remove(storyImage.Image);

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StoryImage>> GetStoryImages(Guid storyId)
        {
            return await this.dbContext.StoryImages.Where(si => si.StoryId == storyId).ToListAsync();
        }

        public async Task CreateMissionIdeaImageAsync(Image image, MissionIdea missionIdea)
        {
            var oldMissionIdeaImages = this.dbContext.MissionIdeaImages
                .Where(mi => mi.MissionIdeaId == missionIdea.Id)
                .Include(mi => mi.Image);

            if (oldMissionIdeaImages != null)
            {
                foreach (var missionIdeaImage in oldMissionIdeaImages)
                {
                    await this.DeleteMissionIdeaImageAsync(missionIdeaImage);
                }
            }

            this.dbContext.Images.Add(image);
            this.dbContext.MissionIdeaImages.Add(new MissionIdeaImage()
            {
                MissionIdea = missionIdea,
                Image = image
            });

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteMissionIdeaImageAsync(MissionIdeaImage missionIdeaImage, bool commit = false)
        {
            this.dbContext.Images.Remove(missionIdeaImage.Image);

            if (commit)
            {
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<MissionIdeaImage> GetMissionIdeaImageAsync(Guid missionIdeaId)
        {
            return await this.dbContext.MissionIdeaImages
                .Where(mi => mi.MissionIdeaId == missionIdeaId)
                .Include(mi => mi.Image)
                .FirstOrDefaultAsync();
        }

        public string GetMissionImageSource(Mission mission)
        {
            if (mission.MissionImages != null && mission.MissionImages.Count > 0)
            {
                var missionImage = mission.MissionImages.FirstOrDefault();
                string imageSrc = this.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);

                return imageSrc;
            }

            return String.Empty;
        }

        public IEnumerable<string> GetStoryImageSources(Story story)
        {
            if (story == null)
            {
                return null;
            }
            else
            {
                var storyImages = story.StoryImages;
                var imageSources = storyImages.Select(si => this.GetImageSource(si.Image.ContentType, si.Image.Bytes)).ToList();

                return imageSources;
            }
        }

        public string GetMissionIdeaImageSource(MissionIdea missionIdea)
        {
            if (missionIdea.MissionIdeaImages != null && missionIdea.MissionIdeaImages.Count > 0)
            {
                var missionImage = missionIdea.MissionIdeaImages.FirstOrDefault();
                string imageSrc = this.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);

                return imageSrc;
            }

            return String.Empty;
        }
    }
}