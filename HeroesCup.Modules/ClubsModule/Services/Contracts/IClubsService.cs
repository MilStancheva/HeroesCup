using ClubsModule.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IClubsService
    {
        Task<ClubListModel> GetClubListModelAsync(Guid ownerId);

        Task<ClubEditModel> CreateClubEditModel(Guid ownerId);

        Task<ClubEditModel> GetClubEditModelByIdAsync(Guid id, Guid ownerId);

        Task<Guid> SaveClubEditModel(ClubEditModel model);
    }
}
