using ClubsModule.Common;
using ClubsModule.Models;
using HeroesCup.Data.Models;
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
        private readonly ClubsModule.Services.Contracts.IStoriesService storiesService;
        private readonly ClubsModule.Services.Contracts.IImagesService imageService;

        public MissionsService(
            ClubsModule.Services.Contracts.IMissionsService missionsService,
            ClubsModule.Services.Contracts.IMissionIdeasService missionIdeasService,
            ClubsModule.Services.Contracts.IStoriesService storiesService,
            ClubsModule.Services.Contracts.IImagesService imageService)
        {
            this.missionsService = missionsService;
            this.missionIdeasService = missionIdeasService;
            this.storiesService = storiesService;
            this.imageService = imageService;
        }

        public IEnumerable<MissionIdeaViewModel> GetMissionIdeaViewModels()
        {
            var timeheroesMissions = this.missionIdeasService.GetAllPublishedMissionIdeas();
            return timeheroesMissions.Select(mi => this.MapMissionIdeaToMissionIdeaViewModel(mi));
        }

        public IEnumerable<MissionViewModel> GetMissionViewModels()
        {
            var missions = this.missionsService.GetAllPublishedMissions();
            return missions.Select(m => this.MapMissionToMissionViewModel(m));
        }

        public async Task<IEnumerable<MissionViewModel>> GetPinnedMissionViewModels()
        {
            var pinnedMissions = await this.missionsService.GetPinnedMissions();
            return pinnedMissions.Select(m => this.MapMissionToMissionViewModel(m));
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
                .Select(m => this.MapMissionToMissionViewModel(m));
        }

        public async Task<MissionViewModel> GetMissionViewModelByIdAsync(Guid id)
        {
            var result = await this.missionsService.GetMissionEditModelByIdAsync(id, null);
            if (result == null)
            {
                return null;
            }

            var model = this.MapMissionEditModelToMissionViewModel(result);

            return model;
        }

        public async Task<MissionIdeaViewModel> GetMissionIdeaViewModelByIdAsync(Guid id)
        {
            var result = await this.missionIdeasService.GetMissionIdeaEditModelByIdAsync(id);
            if (result == null)
            {
                return null;
            }

            var model = this.MapMissionIdeaEditModelToMissionIdeaViewModel(result);

            return model;
        }

        public IEnumerable<StoryViewModel> GetAllPublishedStoryViewModels()
        {
            return this.storiesService.GetAllPublishedStories().Select(s => this.MapStoryToStoryViewModel(s));
        }

        public async Task<StoryViewModel> GetStoryViewModelByIdAsync(Guid id)
        {
            var story = await this.storiesService.GetStoryByIdAsync(id);
            if (story == null)
            {
                return null;
            }

            var model = this.MapStoryToStoryViewModel(story);

            return model;
        }

        public async Task<MissionViewModel> GetMissionViewModelBySlugAsync(string slug)
        {
            var result = await this.missionsService.GetMissionEditModelBySlugAsync(slug);
            if (result == null)
            {
                return null;
            }

            var model = this.MapMissionEditModelToMissionViewModel(result);

            return model;
        }

        public async Task<StoryViewModel> GetStoryViewModelByMissionSlugAsync(string missionSlug)
        {
            var story = await this.storiesService.GetStoryByMissionSlugAsync(missionSlug);
            if (story == null)
            {
                return null;
            }

            var model = this.MapStoryToStoryViewModel(story);

            return model;
        }

        public async Task<MissionIdeaViewModel> GetMissionIdeaViewModelBySlugAsync(string slug)
        {
            var result = await this.missionIdeasService.GetMissionIdeaEditModelBySlugAsync(slug);
            if (result == null)
            {
                return null;
            }

            var model = this.MapMissionIdeaEditModelToMissionIdeaViewModel(result);

            return model;
        }

        private MissionIdeaViewModel MapMissionIdeaToMissionIdeaViewModel(MissionIdea missionIdea)
        {
            if (missionIdea == null)
            {
                return null;
            }

            return new MissionIdeaViewModel()
            {
                Id = missionIdea.Id,
                Slug = missionIdea.Slug,
                MissionIdea = missionIdea,
                ImageSrc = this.imageService.GetMissionIdeaImageSource(missionIdea)
            };
        }

        private MissionIdeaViewModel MapMissionIdeaEditModelToMissionIdeaViewModel(MissionIdeaEditModel missionIdeEditModel)
        {
            if (missionIdeEditModel == null)
            {
                return null;
            }

            return new MissionIdeaViewModel()
            {
                Id = missionIdeEditModel.MissionIdea.Id,
                Slug = missionIdeEditModel.MissionIdea.Slug,
                ImageSrc = missionIdeEditModel.ImageSrc,
                ImageFilename = missionIdeEditModel.ImageFilename,
                MissionIdea = missionIdeEditModel.MissionIdea,
                StartDate = missionIdeEditModel.MissionIdea.StartDate.ConvertToLocalDateTime(),
                EndDate = missionIdeEditModel.MissionIdea.EndDate.ConvertToLocalDateTime()
            };
        }

        private StoryViewModel MapStoryToStoryViewModel(Story story)
        {
            if (story == null)
            {
                return null;
            }

            var imageSources = this.imageService.GetStoryImageSources(story);
            var missionImageSource = this.imageService.GetMissionImageSource(story.Mission);
            var heroImageFilename = story.StoryImages != null && story.StoryImages.Any() ? 
                story.StoryImages.FirstOrDefault().Image.Filename : 
                story.Mission.MissionImages.FirstOrDefault().Image.Filename;

            string heroImageSrc;
            if (imageSources != null && imageSources.Any())
            {
                heroImageSrc = imageSources.FirstOrDefault();
            }
            else
            {
                heroImageSrc = missionImageSource;
            }

            return new StoryViewModel()
            {
                Id = story.Id,
                Content = story.Content,
                ImageSources = imageSources,
                HeroImageSource = heroImageSrc,
                HeroImageFilename = heroImageFilename,
                Mission = new MissionViewModel()
                {
                    Id = story.Mission.Id,
                    Title = story.Mission.Title,
                    Slug = story.Mission.Slug,
                    Club = story.Mission.Club,
                    ImageSrc = missionImageSource,
                    StartDate = story.Mission.StartDate.ConvertToLocalDateTime(),
                    EndDate = story.Mission.EndDate.ConvertToLocalDateTime(),
                }
            };
        }

        private MissionViewModel MapMissionEditModelToMissionViewModel(MissionEditModel missionEditModel)
        {
            if (missionEditModel == null)
            {
                return null;
            }

            return new MissionViewModel()
            {
                Id = missionEditModel.Mission.Id,
                Title = missionEditModel.Mission.Title,
                Slug = missionEditModel.Mission.Slug,
                ImageSrc = missionEditModel.ImageSrc,
                ImageFilename = missionEditModel.ImageFilename,
                Mission = missionEditModel.Mission,
                Club = missionEditModel.Mission.Club,
                StartDate = missionEditModel.Mission.StartDate.ConvertToLocalDateTime(),
                EndDate = missionEditModel.Mission.EndDate.ConvertToLocalDateTime(),
                Story = new StoryViewModel()
                {
                    Content = missionEditModel.Mission.Story != null ? missionEditModel.Mission.Story.Content : null,
                    ImageSources = this.imageService.GetStoryImageSources(missionEditModel.Mission.Story)
                }
            };
        }

        private MissionViewModel MapMissionToMissionViewModel(Mission mission)
        {
            if (mission == null)
            {
                return null;
            }

            return new MissionViewModel()
            {
                Id = mission.Id,
                Title = mission.Title,
                Slug = mission.Slug,
                Club = mission.Club,
                ImageSrc = this.imageService.GetMissionImageSource(mission),
                StartDate = mission.StartDate.ConvertToLocalDateTime(),
                EndDate = mission.EndDate.ConvertToLocalDateTime(),
                Story = new StoryViewModel()
                {
                    Content = mission.Story != null ? mission.Story.Content : null,
                    ImageSources = this.imageService.GetStoryImageSources(mission.Story)
                }
            };
        }
    }
}
