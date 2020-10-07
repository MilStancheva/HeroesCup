using HeroesCup.Data.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IClubImagesService
    {
        Task CreateClubImageAsync(Image image, Club club);

        Task<ClubImage> GetClubImage(Guid clubId);

        Task DeleteClubImageAsync(ClubImage clubImage, bool commit = false);

        string getClubImageId(Guid clubId);
    }
}