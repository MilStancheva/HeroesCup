using ClubsModule.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IMissionsService
    {
        Task<MissionListModel> GetMissionListModelAsync(Guid? ownerId);

        Task<MissionEditModel> CreateMissionEditModelAsync(Guid? ownerId);

        Task<Guid> SaveMissionEditModelAsync(MissionEditModel model);

        Task<bool> PublishMissionEditModelAsync(Guid missionId);

        Task<bool> UnpublishMissionEditModelAsync(Guid missionId);

        Task<MissionEditModel> GetMissionEditModelByIdAsync(Guid id, Guid? ownerId);

        Task<bool> DeleteAsync(Guid id);
    }
}
