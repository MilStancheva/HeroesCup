using ClubsModule.Models;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
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

        TimeSpan GetMissionDuration(long startDate, long endDate);

        Task<IEnumerable<Mission>> GetMissionsBySchoolYear(string schoolYear);

        IEnumerable<string> GetMissionSchoolYears();

        IEnumerable<Mission> GetAllHeroesCupPublishedMissions();
    }
}