using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var missions = this.missionsService.GetAllHeroesCupPublishedMissions();
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
    }
}
