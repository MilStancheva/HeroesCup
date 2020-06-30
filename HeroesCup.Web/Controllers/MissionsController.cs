using HeroesCup.Models;
using HeroesCup.Web.Models;
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
        private const string PageCountKey = "pageCount";
        private readonly IApi api;
        private readonly IModelLoader loader;
        private readonly IMissionsService missionsService;
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
            IConfiguration configuration)
        {
            this.api = api;
            this.loader = loader;
            this.missionsService = missionsService;
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

            int? currentPageCount = null;

            if (loadRequest == true)
            {
                currentPageCount = HttpContext.Session.GetInt32(PageCountKey);
                if (currentPageCount == null)
                {
                    currentPageCount = 2;
                    HttpContext.Session.SetInt32(PageCountKey, (int)currentPageCount);
                }
                else
                {
                    HttpContext.Session.SetInt32(PageCountKey, (int)(currentPageCount += 1));
                }
            }
            else
            {
                currentPageCount = 1;
                HttpContext.Session.SetInt32(PageCountKey, (int)currentPageCount);
            }


            model.MissionIdeas = this.missionsService.GetMissionIdeaViewModels().Take((int)currentPageCount * _missionsCount);
            model.Missions = this.missionsService.GetMissionViewModels().Take((int)currentPageCount * _missionsCount);

            return View(model);
        }


        [Route("missions/load-missionideas")]
        public IActionResult LoadMissionIdeas(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {

            int? currentPageCount = null;
            int? lastPageCount = null;

            if (loadRequest == true)
            {
                currentPageCount = HttpContext.Session.GetInt32(PageCountKey);
                lastPageCount = currentPageCount;
                if (currentPageCount == null)
                {
                    currentPageCount = 2;
                    HttpContext.Session.SetInt32(PageCountKey, (int)currentPageCount);
                }
                else
                {
                    HttpContext.Session.SetInt32(PageCountKey, (int)(currentPageCount += 1));
                }
            }
            else
            {
                currentPageCount = 1;
                HttpContext.Session.SetInt32(PageCountKey, (int)currentPageCount);
            }


            var missionIdeas = this.missionsService.GetMissionIdeaViewModels()
                //.Skip((int)lastPageCount * _missionsCount)
                .Take((int)currentPageCount * _missionsCount);

            return PartialView("_MissionIdeasList", missionIdeas);
        }
    }
}