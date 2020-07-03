using HeroesCup.Data.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IMissionContentsService
    {
        Task<MissionContent> GetMissionContentByMissionId(Guid missionId);

        Task SaveOrUpdateMissionContent(MissionContent missionContent, Mission mission, bool commit = false);
    }
}
