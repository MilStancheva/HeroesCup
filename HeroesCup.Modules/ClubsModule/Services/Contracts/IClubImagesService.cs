using HeroesCup.Data.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IImagesService
    {
        Task CreateClubImageAsync(Image image, Club club);

        Task<Image> GetImage(Guid id);

        Task DeleteClubImageAsync(Guid id);

        string GetImageSource(string contentType, byte[] bytes);
    }
}