using ClubsModule.Common;
using HeroesCup.Web.Models;
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

        public IEnumerable<MissionIdeaViewModel> GetMissionIdeaViewModels()
        {
            var timeheroesMissions = this.missionIdeasService.GetAllPublishedMissionIdeas();
            return timeheroesMissions.Select(mi => new MissionIdeaViewModel()
            {
                Id = mi.Id,
                MissionIdea = mi,
                ImageSrc = this.imageService.GetMissionIdeaImageSource(mi)
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
                ImageSrc = this.imageService.GetMissionImageSource(m),
                StartDate = m.StartDate.ConvertToLocalDateTime(),
                EndDate = m.EndDate.ConvertToLocalDateTime(),
                Story = new StoryViewModel()
                {
                    Content = m.Story != null ? m.Story.Content : null,
                    ImageSources = this.imageService.GetStoryImageSources(m.Story)
                }
            });
        }

        public async Task<IEnumerable<MissionViewModel>> GetPinnedMissionViewModels()
        {
            var pinnedMissions = await this.missionsService.GetPinnedMissions();
            return pinnedMissions.Select(m => new MissionViewModel()
            {
                Id = m.Id,
                Title = m.Title,
                Club = m.Club,
                ImageSrc = this.imageService.GetMissionImageSource(m),
                StartDate = m.StartDate.ConvertToLocalDateTime(),
                EndDate = m.EndDate.ConvertToLocalDateTime(),
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

        public IEnumerable<MissionViewModel> GetMissionViewModelsByLocation(string location)
        {
            return this.missionsService.GetAllPublishedMissions()
                .Where(m => m.Location.Contains(location) || location.Contains(m.Location))
                .Select(m => new MissionViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Club = m.Club,
                    ImageSrc = this.imageService.GetMissionImageSource(m),
                    StartDate = m.StartDate.ConvertToLocalDateTime(),
                    EndDate = m.EndDate.ConvertToLocalDateTime(),
                    Story = new StoryViewModel()
                    {
                        Content = m.Story != null ? m.Story.Content : null,
                        ImageSources = this.imageService.GetStoryImageSources(m.Story)
                    }
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
                StartDate = result.Mission.StartDate.ConvertToLocalDateTime(),
                EndDate = result.Mission.EndDate.ConvertToLocalDateTime(),
                Story = new StoryViewModel()
                {
                    Content = result.Mission.Story != null ? result.Mission.Story.Content : null,
                    ImageSources = this.imageService.GetStoryImageSources(result.Mission.Story)
                }
            };

            return model;
        }

        public async Task<MissionIdeaViewModel> GetMissionIdeaViewModelByIdAsync(Guid id)
        {
            var result = await this.missionIdeasService.GetMissionIdeaEditModelByIdAsync(id);
            var model = new MissionIdeaViewModel()
            {
                Id = result.MissionIdea.Id,
                ImageSrc = result.ImageSrc,
                MissionIdea = result.MissionIdea,
                StartDate = result.MissionIdea.StartDate.ConvertToLocalDateTime(),
                EndDate = result.MissionIdea.EndDate.ConvertToLocalDateTime()
            };

            return model;
        }
    }
}
