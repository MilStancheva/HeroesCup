using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IImagesService
    {
        Task CreateClubImageAsync(Image image, Club club);

        Task<Image> GetImage(Guid id);

        Task<ClubImage> GetClubImage(Guid clubId);

        Task DeleteClubImageAsync(ClubImage clubImage, bool commit = false);

        string GetImageSource(string contentType, byte[] bytes);

        byte[] GetByteArrayFromImage(IFormFile file);

        string GetFilename(IFormFile file);

        string GetFileContentType(IFormFile file);
    }
}