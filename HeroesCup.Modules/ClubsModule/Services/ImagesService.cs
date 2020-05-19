using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
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
    }
}