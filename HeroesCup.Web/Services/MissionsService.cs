using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public class MissionsService : IMissionsService
    {
        private readonly ClubsModule.Services.Contracts.IMissionsService missionsService;
        private readonly ClubsModule.Services.Contracts.IMissionIdeasService missionIdeasService;
        private readonly ClubsModule.Services.Contracts.IImagesService imageService;

        public MissionsService(
            ClubsModule.Services.Contracts.IMissionsService missionsService,
            ClubsModule.Services.Contracts.IMissionIdeasService missionIdeasService,
            ClubsModule.Services.Contracts.IImagesService imageService)
        {
            this.missionsService = missionsService;
            this.missionIdeasService = missionIdeasService;
            this.imageService = imageService;
        }

        public IEnumerable<MissionIdea> GetMissionIdeas()
        {
            var timeheroesMissions = this.missionIdeasService.GetAllPublishedMissionIdeas();
            return timeheroesMissions;
        }

        public IEnumerable<MissionIdeaViewModel> GetMissionIdeaViewModels()
        {
            var timeheroesMissions = this.missionIdeasService.GetAllPublishedMissionIdeas();
            return timeheroesMissions.Select(mi => new MissionIdeaViewModel()
            {
                MissionIdea = mi,
                ImageSrc = GetMissionIdeaImageSource(mi)
            });
        }

        public IEnumerable<MissionViewModel> GetMissionViewModels()
        {
            var missions = this.missionsService.GetAllPublishedMissions();
            return missions.Select(m => new MissionViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Club = m.Club,
                ImageSrc = GetMissionImageSource(m)
            });
        }

        private string GetMissionImageSource(Mission mission)
        {
            if (mission.MissionImages != null && mission.MissionImages.Count > 0)
            {
                var missionImage = mission.MissionImages.FirstOrDefault();
                string imageSrc = this.imageService.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);

                return imageSrc;
            }

            return String.Empty;
        }

        private string GetMissionIdeaImageSource(MissionIdea missionIdea)
        {
            if (missionIdea.MissionIdeaImages != null && missionIdea.MissionIdeaImages.Count > 0)
            {
                var missionImage = missionIdea.MissionIdeaImages.FirstOrDefault();
                string imageSrc = this.imageService.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);

                return imageSrc;
            }

            return String.Empty;
        }

        public async Task<IEnumerable<MissionViewModel>> GetPinnedMissions()
        {
            var pinnedMissions = await this.missionsService.GetPinnedMissions();
            return pinnedMissions.Select(m => new MissionViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Club = m.Club,
                ImageSrc = GetMissionImageSource(m)
            });
        }

        public int GetAllMissionsCount()
        {
            return this.missionsService.GetAllPublishedMissions().Count();
        }

        public IDictionary<string, int> GetMissionsPerLocation()
        {
            return this.missionsService.GetAllPublishedMissions()
                .GroupBy(m => m.Location)
                .ToDictionary(x => x.Key, x => x.Count());
        }

        public IEnumerable<MissionViewModel> GetMissionsByLocation(string location)
        {
            return this.missionsService.GetAllPublishedMissions()
                .Where(m => m.Location.Contains(location) || location.Contains(m.Location))
                .Select(m => new MissionViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Club = m.Club,
                    ImageSrc = GetMissionImageSource(m)
                });
        }

        public async Task<MissionViewModel> GetMissionViewModelByIdAsync(Guid id)
        {
            var result = await this.missionsService.GetMissionEditModelByIdAsync(id, null);
            var model = new MissionViewModel()
            {
                Id = result.Mission.Id,
                Title = result.Mission.Title,
                ImageSrc = result.ImageSrc,
                Mission = result.Mission,
                Club = result.Mission.Club,
                StartDate = result.UploadedStartDate,
                EndDate = result.UploadedEndDate
            };

            return model;
        }
    }
}
