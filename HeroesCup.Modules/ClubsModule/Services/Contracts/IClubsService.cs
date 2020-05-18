using ClubsModule.Models;
using HeroesCup.Data.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IClubsService
    {
        Task<ClubListModel> GetClubListModelAsync(Guid? ownerId);

        Task<ClubEditModel> CreateClubEditModelAsync(Guid? ownerId);

        Task<ClubEditModel> GetClubEditModelByIdAsync(Guid id, Guid? ownerId);

        Task<Guid> SaveClubEditModelAsync(ClubEditModel model);

        Task<Hero> GetClubCoordinatorAsync(Guid clubId);

        Task<bool> DeleteAsync(Guid id);
    }
}
