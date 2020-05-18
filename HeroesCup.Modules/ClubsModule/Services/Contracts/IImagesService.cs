using HeroesCup.Data.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IImagesService
    {
        Task Create(Image image);

        Task<Image> GetImage(Guid id);

        Task DeleteImage(Guid id);

        string GetImageSource(string contentType, byte[] bytes);
    }
}
