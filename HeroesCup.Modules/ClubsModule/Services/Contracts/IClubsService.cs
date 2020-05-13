using ClubsModule.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IClubsService
    {
        Task<ClubListModel> GetClubListModelAsync();

        ClubEditModel CreateClubEditModel();

        Task<ClubEditModel> GetClubEditModelByIdAsync(Guid id);

        Task<bool> SaveClubEditModel(ClubEditModel model);
    }
}
