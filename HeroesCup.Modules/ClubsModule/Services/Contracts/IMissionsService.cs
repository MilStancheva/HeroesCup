using ClubsModule.Models;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IMissionsService
    {
        Task<MissionListModel> GetMissionListModelAsync(Guid? ownerId = null);

        Task<MissionEditModel> CreateMissionEditModelAsync(Guid? ownerId = null);

        Task<Guid> SaveMissionEditModelAsync(MissionEditModel model);

        Task<bool> PublishMissionEditModelAsync(Guid missionId);

        Task<bool> UnpublishMissionEditModelAsync(Guid missionId);

        Task<MissionEditModel> GetMissionEditModelByIdAsync(Guid id, Guid? ownerId = null);

        Task<MissionEditModel> GetMissionEditModelBySlugAsync(String slug);

        Task<bool> DeleteAsync(Guid id);

        TimeSpan GetMissionDuration(long startDate, long endDate);

        Task<IEnumerable<Mission>> GetMissionsBySchoolYear(string schoolYear);

        IEnumerable<string> GetMissionSchoolYears();

        IEnumerable<Mission> GetAllPublishedMissions();

        Task<bool> PinMissionEditModelAsync(Guid id);

        Task<bool> UnpinMissionEditModelAsync(Guid id);

        Task<IEnumerable<Mission>> GetPinnedMissions();

        Task SaveMissionDurationHours(Mission mission, int durationHours, bool commit = false);

        Task SaveMissionHeroes(Mission mission, IEnumerable<Guid> ids, bool commit = false);
    }
}