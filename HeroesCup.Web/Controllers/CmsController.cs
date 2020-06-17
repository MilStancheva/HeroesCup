using HeroesCup.Models;
using HeroesCup.Web.Models;
using HeroesCup.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Controllers
{
    public class CmsController : Controller
    {
        private readonly IApi api;
        private readonly IModelLoader loader;
        private readonly ILeaderboardService leaderboardService;
        private readonly IStatisticsService statisticsService;
        private readonly ITimeheroesMissionsService missionsService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public CmsController(
            IApi api, 
            IModelLoader loader, 
            ILeaderboardService leaderboardService, 
            IStatisticsService statisticsService,
            ITimeheroesMissionsService missionsService)
        {
            this.api = api;
            this.loader = loader;
            this.leaderboardService = leaderboardService;
            this.statisticsService = statisticsService;
            this.missionsService = missionsService;
        }

        /// <summary>
        /// Gets the blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("archive")]
        public async Task<IActionResult> Archive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<BlogArchive>(id, HttpContext.User, draft);
            model.Archive = await this.api.Archives.GetByIdAsync(id, page, category, tag, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("page")]
        public async Task<IActionResult> Page(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<StandardPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the post with the given id.
        /// </summary>
        /// <param name="id">The unique post id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("post")]
        public async Task<IActionResult> Post(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPostAsync<BlogPost>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the startpage with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("/")]
        public async Task<IActionResult> Start(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<StartPage>(id, HttpContext.User, draft);

            // Leaderboard
            model.SchoolYears = this.leaderboardService.GetSchoolYears();
            model.SelectedSchoolYear = this.leaderboardService.GetCurrentSchoolYear();
            var clubsListModel = await this.leaderboardService.GetClubsBySchoolYearAsync(model.SelectedSchoolYear);
            model.Clubs = clubsListModel;

            // Statistics
            model.MissionsCount = this.statisticsService.GetAllMissionsCount();
            model.ClubsCount = this.statisticsService.GetAllClubsCount();
            model.HeroesCount = this.statisticsService.GetAllHeroesCount();
            model.HoursCount = this.statisticsService.GetAllHoursCount();

            // Missions
            model.TimeheroesMissions = this.missionsService.GetTimeheroesMissions().TakeLast(3);

            return View(model);
        }

        /// <summary>
        /// Gets the startpage with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Start(Guid id, String selectedSchoolYear, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<StartPage>(id, HttpContext.User, draft);

            // Leaderboard
            model.SchoolYears = this.leaderboardService.GetSchoolYears();
            model.SelectedSchoolYear = selectedSchoolYear;
            var clubsListModel = await this.leaderboardService.GetClubsBySchoolYearAsync(selectedSchoolYear);
            model.Clubs = clubsListModel;

            // Statistics
            model.MissionsCount = this.statisticsService.GetAllMissionsCount();
            model.ClubsCount = this.statisticsService.GetAllClubsCount();
            model.HeroesCount = this.statisticsService.GetAllHeroesCount();
            model.HoursCount = this.statisticsService.GetAllHoursCount();


            return View(model);
        }


        /// <summary>
        /// Gets the link-mission with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("link-mission")]
        public async Task<IActionResult> LinkMissionPost(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPostAsync<LinkMissionPost>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the blog-mission with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("blog-mission")]
        public async Task<IActionResult> BlogMissionPost(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPostAsync<BlogMissionPost>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the missions archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("missions")]
        public async Task<IActionResult> MissionsArchive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<MissionsArchive>(id, HttpContext.User, draft);
            model.LinkMissionArchive = await this.api.Archives.GetByIdAsync<LinkMissionPost>(id, page, category, tag, year, month);
            model.BlogMissionArchive = await this.api.Archives.GetByIdAsync<BlogMissionPost>(id, page, category, tag, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the landing page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("/landing")]
        public async Task<IActionResult> LandingPage(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<LandingPage>(id, HttpContext.User, draft);

            return View(model);
        }

        /// <summary>
        /// Gets the about page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("/about")]
        public async Task<IActionResult> AboutPage(Guid id, bool draft = false)
        {
            var model = await this.loader.GetPageAsync<AboutPage>(id, HttpContext.User, draft);

            return View(model);
        }
    }
}