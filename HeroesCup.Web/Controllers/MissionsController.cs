﻿using HeroesCup.Models;
using HeroesCup.Web.Common.Extensions;
using HeroesCup.Web.Models.Missions;
using HeroesCup.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Piranha;
using Piranha.AspNetCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Controllers
{
    public class MissionsController : Controller
    {
        private const string MissionsPageCountKey = "missionsPageCount";
        private const string MissionIdeasPageCountKey = "missionIdeasPageCount";
        private const string StoriesPageCountKey = "storiesPageCount";
        private readonly IApi api;
        private readonly IModelLoader loader;
        private readonly IMissionsService missionsService;
        private readonly ISessionService sessionService;
        private readonly IConfiguration _configuration;
        private int _missionsCount;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public MissionsController(
            IApi api,
            IModelLoader loader,
            IMissionsService missionsService,
            ISessionService sessionService,
            IConfiguration configuration)
        {
            this.api = api;
            this.loader = loader;
            this.missionsService = missionsService;
            this.sessionService = sessionService;
            _configuration = configuration;
            int.TryParse(_configuration["MissionsCount"], out _missionsCount);
        }

        /// <summary>
        /// Gets the missions archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="loadRequest">The optional load more missions</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("missions")]
        public async Task<IActionResult> MissionsArchive(Guid id, bool loadRequest, string selectedLocation, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<MissionsPage>(id, HttpContext.User, draft);

            int missionsCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionsPageCountKey);
            int missionIdeasCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionIdeasPageCountKey);
            int storiesCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, StoriesPageCountKey);

            if (selectedLocation != null)
            {
                model.SelectedLocation = selectedLocation.Trim();
                model.Missions = this.missionsService.GetMissionViewModelsByLocation(selectedLocation); 
            }
            else if (loadRequest)
            {
                model.IsLoadMoreMissionsRequest = true;
                model.Missions = this.missionsService.GetMissionViewModels().ToList();
            }
            else
            {
                model.Missions = this.missionsService.GetMissionViewModels().Take((int)missionsCurrentPageCount * _missionsCount);
            }

            model.MissionIdeas = this.missionsService.GetMissionIdeaViewModels().Take((int)missionIdeasCurrentPageCount * _missionsCount);
            model.Stories = this.missionsService.GetAllPublishedStoryViewModels().Take((int)storiesCurrentPageCount * _missionsCount);

            model.MissionsPerLocation = this.missionsService.GetMissionsPerLocation();
            model.MissionsCount = this.missionsService.GetAllMissionsCount();

            return View(model);
        }

        //[Route("mission/{id}")]
        //public async Task<IActionResult> MissionPost(Guid id, bool draft = false)
        //{
        //    var mission = await this.missionsService.GetMissionViewModelByIdAsync(id);
        //    var model = new MissionPost()
        //    {
        //        Mission = mission,
        //        Title = mission.Mission.Title,
        //        Slug = mission.Mission.Title,
        //        Category = "mission",
        //    };

        //    return View(model);
        //}

        [Route("mission/{slug}")]
        public async Task<IActionResult> MissionPost(String slug, bool draft = false)
        {
            var mission = await this.missionsService.GetMissionViewModelBySlugAsync(slug);
            var model = new MissionPost()
            {
                Mission = mission,
                Title = mission.Mission.Title,
                Slug = mission.Mission.Slug,
                Category = "mission",
            };

            return View(model);
        }

        //[Route("mission-idea/{id}")]
        //public async Task<IActionResult> MissionIdeaPost(Guid id, bool draft = false)
        //{
        //    var missionIdea = await this.missionsService.GetMissionIdeaViewModelByIdAsync(id);
        //    var model = new MissionIdeaPost()
        //    {
        //        MissionIdea = missionIdea,
        //        Title = missionIdea.MissionIdea.Title,
        //        Slug = missionIdea.MissionIdea.Title,
        //        Category = "mission-idea",
        //    };

        //    return View(model);
        //}

        [Route("mission-idea/{slug}")]
        public async Task<IActionResult> MissionIdeaPost(string slug, bool draft = false)
        {
            var missionIdea = await this.missionsService.GetMissionIdeaViewModelBySlugAsync(slug);
            var model = new MissionIdeaPost()
            {
                MissionIdea = missionIdea,
                Title = missionIdea.MissionIdea.Title,
                Slug = missionIdea.MissionIdea.Slug,
                Category = "mission-idea",
            };

            return View(model);
        }

        //[Route("story/{id}")]
        //public async Task<IActionResult> StoryPost(Guid id, bool draft = false)
        //{
        //    var story = await this.missionsService.GetStoryViewModelByIdAsync(id);
        //    var model = new StoryPost()
        //    {
        //        Story = story,
        //        Title = story.Mission.Title,
        //        Slug = story.Mission.Title,
        //        Category = "story",
        //    };

        //    return View(model);
        //}

        [Route("story/{slug}")]
        public async Task<IActionResult> StoryPost(string slug, bool draft = false)
        {
            var story = await this.missionsService.GetStoryViewModelByMissionSlugAsync(slug);
            var model = new StoryPost()
            {
                Story = story,
                Title = story.Mission.Title,
                Slug = story.Mission.Slug,
                Category = "story",
            };

            return View(model);
        }

        [Route("missions/load-missions")]
        public IActionResult LoadMissions(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            int missionsCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionsPageCountKey);
            var missions = this.missionsService.GetMissionViewModels()
                 .Skip(_missionsCount);

            var missionsWithBanner = new MissionsWithBannerViewModel()
            {
                Missions = missions,
                MissionsCountPerPage = _missionsCount
            };

            return PartialView("_MissionsListWithBanner", missionsWithBanner);
        }


        [Route("missions/load-missionideas")]
        public IActionResult LoadMissionIdeas(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {

            int missionIdeasCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionIdeasPageCountKey);
            var missionIdeas = this.missionsService.GetMissionIdeaViewModels();
                //.Skip((int)missionIdeasCurrentPageCount * _missionsCount);

            return PartialView("_MissionIdeasList", missionIdeas);
        }

        [Route("missions/load-stories")]
        public IActionResult LoadStories(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {

            int storiesCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, StoriesPageCountKey);
            var stories = this.missionsService.GetAllPublishedStoryViewModels();
                //.Skip((int)storiesCurrentPageCount * _missionsCount);

            return PartialView("_StoriesList", stories);
        }
    }
}