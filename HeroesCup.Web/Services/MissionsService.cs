using ClubsModule.Common;
using ClubsModule.Models;
using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
using HeroesCup.Web.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HeroesCup.Web.Common.Extensions;

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
            return missions.Select(m => this.MapMissionToMissionViewModel(m, this.missionsService.GetMissionImagesIds(m.Id)));
        }

        public async Task<IEnumerable<MissionViewModel>> GetPinnedMissionViewModels()
        {
            var pinnedMissions = await this.missionsService.GetPinnedMissions();
            return pinnedMissions.Select(m => this.MapMissionToMissionViewModel(m, this.missionsService.GetMissionImagesIds(m.Id)));
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
                .Select(m => this.MapMissionToMissionViewModel(m, this.missionsService.GetMissionImagesIds(m.Id)));
        }

        public IEnumerable<StoryViewModel> GetAllPublishedStoryViewModels()
        {
            return this.storiesService.GetAllPublishedStories().Select(s => this.MapStoryToStoryViewModel(s));
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

            var model = this.MapStoryToStoryViewModel(story, true);

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
                ImageId = missionIdea.MissionIdeaImages != null && missionIdea.MissionIdeaImages.Any() ? missionIdea.MissionIdeaImages.FirstOrDefault().ImageId.ToString() : null,
                IsExpired = missionIdea.EndDate.IsExpired(),
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
                ImageId = missionIdeEditModel.ImageId,
                ImageFilename = missionIdeEditModel.ImageFilename,
                MissionIdea = missionIdeEditModel.MissionIdea,
                StartDate = missionIdeEditModel.MissionIdea.StartDate.ConvertToLocalDateTime(),
                EndDate = missionIdeEditModel.MissionIdea.EndDate.ConvertToLocalDateTime(),
                IsExpired = missionIdeEditModel.MissionIdea.EndDate.IsExpired(),
                IsSeveralDays = IsSeveralDays(missionIdeEditModel.MissionIdea.StartDate, missionIdeEditModel.MissionIdea.EndDate),
                Organization = missionIdeEditModel.MissionIdea.Organization != null && missionIdeEditModel.MissionIdea.Organization != String.Empty ? missionIdeEditModel.MissionIdea.Organization : this.configuration["DefaultOrganization"]
            };
        }

        private StoryViewModel MapStoryToStoryViewModel(Story story, bool includeImages = false)
        {
            if (story == null)
            {
                return null;
            }

            var storyImageIds = story.StoryImages.Select(s => s.ImageId.ToString());
            string heroImageId = story.StoryImages != null && story.StoryImages.Any() ?
                story.StoryImages.FirstOrDefault().ImageId.ToString() :
                story.Mission.MissionImages.FirstOrDefault().ImageId.ToString();
            string heroImageFilename = null;

            if (includeImages)
            {
                heroImageFilename = story.StoryImages != null && story.StoryImages.Any() ?
                    story.StoryImages.FirstOrDefault().Image.Filename :
                    story.Mission.MissionImages.FirstOrDefault().Image.Filename;
            }


            return new StoryViewModel()
            {
                Id = story.Id,
                Content = story.Content,
                ClubName = story.Mission.Club.Name,
                HeroImageFilename = heroImageFilename,
                ImageIds = storyImageIds,
                HeroImageId = heroImageId,
                Mission = new MissionViewModel()
                {
                    Id = story.Mission.Id,
                    Title = story.Mission.Title,
                    Slug = story.Mission.Slug,
                    ClubName = story.Mission.Club.Name,
                    PostClubName = GetPostClubName(story.Mission.Club),
                    ClubLocation = story.Mission.Club.Location,
                    IsExpired = story.Mission.EndDate.IsExpired(),
                    IsSeveralDays = IsSeveralDays(story.Mission.StartDate, story.Mission.EndDate),
                    ImageFilename = this.imageService.GetImageFilename(story.Mission.MissionImages.FirstOrDefault() != null ? story.Mission.MissionImages.FirstOrDefault().Image : null),
                    ImageId = story.Mission.MissionImages != null && story.Mission.MissionImages.Any() ? story.Mission.MissionImages.FirstOrDefault().ImageId.ToString() : null,
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
                ImageId = missionEditModel.ImageId,
                Content = missionEditModel.Mission.Content,
                ClubName = missionEditModel.Mission.Club.Name,
                PostClubName = GetPostClubName(missionEditModel.Mission.Club),
                ClubLocation = missionEditModel.Mission.Club.Location,
                StartDate = missionEditModel.Mission.StartDate.ConvertToLocalDateTime(),
                EndDate = missionEditModel.Mission.EndDate.ConvertToLocalDateTime(),
                IsExpired = missionEditModel.Mission.EndDate.IsExpired(),
                IsSeveralDays = IsSeveralDays(missionEditModel.Mission.StartDate, missionEditModel.Mission.EndDate),
                Story = missionEditModel.Mission.Story != null ? new StoryViewModel()
                {
                    Content = missionEditModel.Mission.Story != null ? missionEditModel.Mission.Story.Content : null,
                    ClubName = missionEditModel.Mission.Club.Name,
                    ImageIds = missionEditModel.Mission.Story.StoryImages != null && missionEditModel.Mission.Story.StoryImages.Any() ? missionEditModel.Mission.Story.StoryImages.Select(s => s.ImageId.ToString()) : null,
                } : null
            };
        }
      
        private MissionViewModel MapMissionToMissionViewModel(Mission mission, IEnumerable<Tuple<string, string>> missionImages)
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
                ClubName = mission.Club.Name,
                PostClubName = GetPostClubName(mission.Club),
                ClubLocation = mission.Club.Location,
                ImageFilename = missionImages.FirstOrDefault().Item2,
                ImageId = missionImages.FirstOrDefault().Item1,
                StartDate = mission.StartDate.ConvertToLocalDateTime(),
                EndDate = mission.EndDate.ConvertToLocalDateTime(),
                IsExpired = mission.EndDate.IsExpired(),
                IsSeveralDays = IsSeveralDays(mission.StartDate, mission.EndDate),
                Story = mission.Story != null ? new StoryViewModel()
                {
                    Content = mission.Story.Content,
                    ClubName = mission.Club.Name,
                    ImageIds = mission.Story.StoryImages != null && mission.Story.StoryImages.Any() ? mission.Story.StoryImages.Select(s => s.ImageId.ToString()) : null,
                } : null                
            };
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

        public string ParseLocation(string location)
        {
            if (location == null)
            {
                return null;
            }

            var parsedResult = Regex.Replace(location, @"(\(\d\))", "");
            return parsedResult.Trim();
        }
    }
}
