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
            return timeheroesMissions.Select(mi => new MissionIdeaViewModel()
            {
                Id = mi.Id,
                Slug = mi.Slug,
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
                Slug = m.Slug,
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
                Slug = m.Slug,
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
                    Slug = m.Slug,
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
                Slug = result.Mission.Slug,
                ImageSrc = result.ImageSrc,
                ImageFilename = result.ImageFilename,
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
                Slug = result.MissionIdea.Slug,
                ImageSrc = result.ImageSrc,
                ImageFilename = result.ImageFilename,
                MissionIdea = result.MissionIdea,
                StartDate = result.MissionIdea.StartDate.ConvertToLocalDateTime(),
                EndDate = result.MissionIdea.EndDate.ConvertToLocalDateTime()
            };

            return model;
        }

        public IEnumerable<StoryViewModel> GetAllPublishedStoryViewModels()
        {
            return this.storiesService.GetAllPublishedStories().Select(s => new StoryViewModel()
            {
                Id = s.Id,
                Content = s.Content,
                ImageSources = this.imageService.GetStoryImageSources(s),
                Mission = new MissionViewModel()
                {
                    Id = s.Mission.Id,
                    Title = s.Mission.Title,
                    Slug = s.Mission.Slug,
                    Club = s.Mission.Club,
                    ImageSrc = this.imageService.GetMissionImageSource(s.Mission),
                    StartDate = s.Mission.StartDate.ConvertToLocalDateTime(),
                    EndDate = s.Mission.EndDate.ConvertToLocalDateTime(),
                }
            });
        }

        public async Task<StoryViewModel> GetStoryViewModelByIdAsync(Guid id)
        {
            var story = await this.storiesService.GetStoryByIdAsync(id);
            var model = new StoryViewModel()
            {
                Id = story.Id,
                Content = story.Content,
                ImageSources = this.imageService.GetStoryImageSources(story),
                Mission = new MissionViewModel()
                {
                    Id = story.Mission.Id,
                    Title = story.Mission.Title,
                    Slug = story.Mission.Slug,
                    Club = story.Mission.Club,
                    ImageSrc = this.imageService.GetMissionImageSource(story.Mission),
                    StartDate = story.Mission.StartDate.ConvertToLocalDateTime(),
                    EndDate = story.Mission.EndDate.ConvertToLocalDateTime(),
                }
            };

            return model;
        }

        public async Task<MissionViewModel> GetMissionViewModelBySlugAsync(string slug)
        {
            var result = await this.missionsService.GetMissionEditModelBySlugAsync(slug);
            if (result == null)
            {
                return null;
            }

            var model = new MissionViewModel()
            {
                Id = result.Mission.Id,
                Title = result.Mission.Title,
                Slug = result.Mission.Slug,
                ImageSrc = result.ImageSrc,
                ImageFilename = result.ImageFilename,
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

        public async Task<StoryViewModel> GetStoryViewModelByMissionSlugAsync(string missionSlug)
        {
            var story = await this.storiesService.GetStoryByMissionSlugAsync(missionSlug);
            if (story == null)
            {
                return null;
            }

            var model = new StoryViewModel()
            {
                Id = story.Id,
                Content = story.Content,
                ImageSources = this.imageService.GetStoryImageSources(story),
                Mission = new MissionViewModel()
                {
                    Id = story.Mission.Id,
                    Title = story.Mission.Title,
                    Slug = story.Mission.Slug,
                    Club = story.Mission.Club,
                    ImageSrc = this.imageService.GetMissionImageSource(story.Mission),
                    ImageFilename = story.Mission.MissionImages.FirstOrDefault().Image.Filename,
                    StartDate = story.Mission.StartDate.ConvertToLocalDateTime(),
                    EndDate = story.Mission.EndDate.ConvertToLocalDateTime(),
                }
            };

            return model;
        }

        public async Task<MissionIdeaViewModel> GetMissionIdeaViewModelBySlugAsync(string slug)
        {
            var result = await this.missionIdeasService.GetMissionIdeaEditModelBySlugAsync(slug);
            if (result == null)
            {
                return null;
            }

            var model = new MissionIdeaViewModel()
            {
                Id = result.MissionIdea.Id,
                Slug = result.MissionIdea.Slug,
                ImageSrc = result.ImageSrc,
                ImageFilename = result.ImageFilename,
                MissionIdea = result.MissionIdea,
                StartDate = result.MissionIdea.StartDate.ConvertToLocalDateTime(),
                EndDate = result.MissionIdea.EndDate.ConvertToLocalDateTime()
            };

            return model;
        }
    }
}
