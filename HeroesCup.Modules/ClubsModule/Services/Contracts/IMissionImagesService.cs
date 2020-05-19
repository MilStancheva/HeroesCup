using HeroesCup.Data.Models;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IMissionImagesService
    {
        Task CreateMissionImageAsync(Image image, Mission mission);

        Task DeleteMissionImageAsync(MissionImage missionImage, bool commit = false);
    }
}
