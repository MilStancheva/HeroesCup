using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IImagesService : IClubImagesService, IMissionImagesService, IStoryImageService, IMissionIdeaImagesService
    {
        Task<Image> GetImage(Guid id);

        Task<Image> GetImageByFileName(string filename);

        string GetImageSource(string contentType, byte[] bytes);

        byte[] GetByteArrayFromImage(IFormFile file);

        string GetFilename(IFormFile file, Guid imageId);

        string GetFileContentType(IFormFile file);

        Image MapFormFileToImage(IFormFile file);

        string GetImageFilename(Image image);

        IEnumerable<string> GetImageFilenames(IEnumerable<Image> images);
    }
}