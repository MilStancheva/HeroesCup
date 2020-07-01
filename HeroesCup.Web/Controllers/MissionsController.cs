using HeroesCup.Models;
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
        public async Task<IActionResult> MissionsArchive(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<MissionsPage>(id, HttpContext.User, draft);

            int missionsCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionsPageCountKey);
            int missionIdeasCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionIdeasPageCountKey);
            
            model.Missions = this.missionsService.GetMissionViewModels().Take((int)missionsCurrentPageCount * _missionsCount);
            model.MissionIdeas = this.missionsService.GetMissionIdeaViewModels().Take((int)missionIdeasCurrentPageCount * _missionsCount);

            model.MissionsPerLocation = this.missionsService.GetMissionsPerLocation();
            model.MissionsCount = this.missionsService.GetAllMissionsCount();

            return View(model);
        }

        [Route("mission/{id}")]
        public async Task<IActionResult> MissionPost(Guid id, bool draft = false)
        {
            var mission = await this.missionsService.GetMissionViewModelByIdAsync(id);
            var model = new MissionPost()
            {
                Mission = mission,
                Title = mission.Mission.Title,
                Slug = mission.Mission.Title,
                Category = "mission",
        };

            return View(model);
        }

        [Route("missions/load-missions")]
        public IActionResult LoadMissions(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {

            int missionsCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionsPageCountKey);
            var missions = this.missionsService.GetMissionViewModels()
                .Take(missionsCurrentPageCount * _missionsCount);

            return PartialView("_MissionsList", missions);
        }


        [Route("missions/load-missionideas")]
        public IActionResult LoadMissionIdeas(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {

            int missionIdeasCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionIdeasPageCountKey);
            var missionIdeas = this.missionsService.GetMissionIdeaViewModels()
                .Take((int)missionIdeasCurrentPageCount * _missionsCount);

            return PartialView("_MissionIdeasList", missionIdeas);
        }

        [Route("missions/missionsbylocation")]
        public async Task<IActionResult> MissionsByLocationAsync(Guid id, string selectedLocation, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<MissionsPage>(id, HttpContext.User, draft);
            model.SelectedLocation = selectedLocation.Trim();
            var missions = this.missionsService.GetMissionViewModelsByLocation(selectedLocation);
            model.Missions = missions;

            int missionIdeasCurrentPageCount = sessionService.GetCurrentPageCount(HttpContext, loadRequest, MissionIdeasPageCountKey);
            model.MissionIdeas = this.missionsService.GetMissionIdeaViewModels().Take((int)missionIdeasCurrentPageCount * _missionsCount);

            model.MissionsPerLocation = this.missionsService.GetMissionsPerLocation();
            model.MissionsCount = this.missionsService.GetAllMissionsCount();

            return View("MissionsArchive", model);
        }
    }
}