using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task Create(Image image)
        {
            var oldImage = this.dbContext.Images.FirstOrDefault(i => i.ClubId == image.ClubId);
            if (oldImage != null)
            {
                await this.DeleteImage(oldImage.Id);
            }

            this.dbContext.Images.Add(image);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteImage(Guid id)
        {
            var image = this.dbContext.Images.FirstOrDefault(i => i.Id == id);
            this.dbContext.Images.Remove(image);
            await this.dbContext.SaveChangesAsync();
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
    }
}
