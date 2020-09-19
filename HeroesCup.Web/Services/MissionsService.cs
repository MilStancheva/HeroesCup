using ClubsModule.Common;
using ClubsModule.Models;
using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public MissionsService(
            ClubsModule.Services.Contracts.IMissionsService missionsService,
            ClubsModule.Services.Contracts.IMissionIdeasService missionIdeasService,
            ClubsModule.Services.Contracts.IStoriesService storiesService,
            ClubsModule.Services.Contracts.IImagesService imageService,
            IConfiguration configuration)
        {
            this.missionsService = missionsService;
            this.missionIdeasService = missionIdeasService;
            this.storiesService = storiesService;
            this.imageService = imageService;
            this.configuration = configuration;
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
                ImageFilename = this.imageService.GetImageFilename(missionIdea.MissionIdeaImages.FirstOrDefault() != null ? missionIdea.MissionIdeaImages.FirstOrDefault().Image : null),
                IsExpired = IsExpired(missionIdea.EndDate),
                IsSeveralDays = IsSeveralDays(missionIdea.StartDate, missionIdea.EndDate),
                Organization = missionIdea.Organization != null && missionIdea.Organization != String.Empty ? missionIdea.Organization : this.configuration["DefaultOrganization"]
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
                ImageFilename = missionIdeEditModel.ImageFilename,
                MissionIdea = missionIdeEditModel.MissionIdea,
                StartDate = missionIdeEditModel.MissionIdea.StartDate.ConvertToLocalDateTime(),
                EndDate = missionIdeEditModel.MissionIdea.EndDate.ConvertToLocalDateTime(),
                IsExpired = IsExpired(missionIdeEditModel.MissionIdea.EndDate),
                IsSeveralDays = IsSeveralDays(missionIdeEditModel.MissionIdea.StartDate, missionIdeEditModel.MissionIdea.EndDate),
                Organization = missionIdeEditModel.MissionIdea.Organization != null && missionIdeEditModel.MissionIdea.Organization != String.Empty ? missionIdeEditModel.MissionIdea.Organization : this.configuration["DefaultOrganization"]
            };
        }

        private StoryViewModel MapStoryToStoryViewModel(Story story)
        {
            if (story == null)
            {
                return null;
            }

            var storyImageFilenames = this.imageService.GetImageFilenames(story.StoryImages.Select(s => s.Image));
            var heroImageFilename = story.StoryImages != null && story.StoryImages.Any() ? 
                story.StoryImages.FirstOrDefault().Image.Filename : 
                story.Mission.MissionImages.FirstOrDefault().Image.Filename;            

            return new StoryViewModel()
            {
                Id = story.Id,
                Content = story.Content,
                ClubName = story.Mission.Club.Name,
                ImageFilenames = storyImageFilenames,
                HeroImageFilename = heroImageFilename,
                Mission = new MissionViewModel()
                {
                    Id = story.Mission.Id,
                    Title = story.Mission.Title,
                    Slug = story.Mission.Slug,
                    //Club = story.Mission.Club,
                    ClubName = story.Mission.Club.Name,
                    PostClubName = GetPostClubName(story.Mission.Club),
                    ClubLocation = story.Mission.Club.Location,
                    IsExpired = IsExpired(story.Mission.EndDate),
                    IsSeveralDays = IsSeveralDays(story.Mission.StartDate, story.Mission.EndDate),
                    ImageFilename = this.imageService.GetImageFilename(story.Mission.MissionImages.FirstOrDefault() != null ? story.Mission.MissionImages.FirstOrDefault().Image : null),
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
                ImageFilename = missionEditModel.ImageFilename,
                Mission = missionEditModel.Mission,
                //Club = missionEditModel.Mission.Club,
                ClubName = missionEditModel.Mission.Club.Name,
                PostClubName = GetPostClubName(missionEditModel.Mission.Club),
                ClubLocation = missionEditModel.Mission.Club.Location,
                StartDate = missionEditModel.Mission.StartDate.ConvertToLocalDateTime(),
                EndDate = missionEditModel.Mission.EndDate.ConvertToLocalDateTime(),
                IsExpired = IsExpired(missionEditModel.Mission.EndDate),
                IsSeveralDays = IsSeveralDays(missionEditModel.Mission.StartDate, missionEditModel.Mission.EndDate),
                Story = missionEditModel.Mission.Story != null ? new StoryViewModel()
                {
                    Content = missionEditModel.Mission.Story != null ? missionEditModel.Mission.Story.Content : null,
                    ClubName = missionEditModel.Mission.Club.Name,
                    ImageFilenames = this.imageService.GetImageFilenames(missionEditModel.Mission.Story.StoryImages.Select(s => s.Image))
                } : null
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
                //Club = mission.Club,
                ClubName = mission.Club.Name,
                PostClubName = GetPostClubName(mission.Club),
                ClubLocation = mission.Club.Location,
                ImageFilename = this.imageService.GetImageFilename(mission.MissionImages.FirstOrDefault() != null ? mission.MissionImages.FirstOrDefault().Image : null),
                StartDate = mission.StartDate.ConvertToLocalDateTime(),
                EndDate = mission.EndDate.ConvertToLocalDateTime(),
                IsExpired = IsExpired(mission.EndDate),
                IsSeveralDays = IsSeveralDays(mission.StartDate, mission.EndDate),
                Story = mission.Story != null ? new StoryViewModel()
                {
                    Content = mission.Story.Content,
                    ClubName = mission.Club.Name,
                    ImageFilenames = this.imageService.GetImageFilenames(mission.Story.StoryImages != null ? mission.Story.StoryImages.Select(s => s.Image) : null)
                } : null                
            };
        }

        private bool IsExpired(long endDate)
        {
            var today = DateTime.Now.Date;
            var expiredMission = false;
            if (today > endDate.ConvertToLocalDateTime().Date)
            {
                expiredMission = true;
            }

            return expiredMission;
        }

        private bool IsSeveralDays(long startDate, long endDate)
        {
            return endDate.ConvertToLocalDateTime().Date != startDate.ConvertToLocalDateTime().Date;
        }

        private string GetPostClubName(Club club)
        {
            var clubNumber = club.OrganizationNumber != null && club.OrganizationNumber != string.Empty ? $" {club.OrganizationNumber} " : string.Empty;
            var clubName = $"\"{club.Name}\", {clubNumber} {club.OrganizationType} \"{club.OrganizationName}\"";

            return clubName;
        }
    }
}
