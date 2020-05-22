using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IImagesService : IClubImagesService, IMissionImagesService, IStoryImageService
    {
        Task<Image> GetImage(Guid id);     

        string GetImageSource(string contentType, byte[] bytes);

        byte[] GetByteArrayFromImage(IFormFile file);

        string GetFilename(IFormFile file);

        string GetFileContentType(IFormFile file);
    }
}