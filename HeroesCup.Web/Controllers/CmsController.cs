using HeroesCup.Models;
using HeroesCup.Models.Regions;
using HeroesCup.Web.Models;
using HeroesCup.Web.Models.Resources;
using HeroesCup.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Controllers
{
    public class CmsController : Controller
    {
        private readonly IApi _api;
        private readonly IModelLoader _loader;
        private readonly ILeaderboardService leaderboardService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="api">The current api</param>
        public CmsController(IApi api, IModelLoader loader, ILeaderboardService leaderboardService)
        {
            _api = api;
            _loader = loader;
            this.leaderboardService = leaderboardService;
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
            var model = await _loader.GetPageAsync<BlogArchive>(id, HttpContext.User, draft);
            model.Archive = await _api.Archives.GetByIdAsync(id, page, category, tag, year, month);

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
            var model = await _loader.GetPageAsync<StandardPage>(id, HttpContext.User, draft);

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
            var model = await _loader.GetPostAsync<BlogPost>(id, HttpContext.User, draft);

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
            var model = await _loader.GetPageAsync<StartPage>(id, HttpContext.User, draft);
            model.SchoolYears = this.leaderboardService.GetSchoolYears();
            model.SelectedSchoolYear = this.leaderboardService.GetCurrentSchoolYear();
            var clubsListModel = await this.leaderboardService.GetClubsBySchoolYearAsync(model.SelectedSchoolYear);
            model.Clubs = clubsListModel;


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
            var model = await _loader.GetPageAsync<StartPage>(id, HttpContext.User, draft);

            // TODO: Filter school clubs by selected school year
            //var pages = _api.Pages.GetAllAsync().Result.ToList();
            //// Get school club regions
            //var schoolClubArchiveId = pages.First(p => p.TypeId == "SchoolClubArchive").Id;
            //var schoolClubPosts = await _api.Posts.GetAllAsync<SchoolClubPost>(schoolClubArchiveId);
            //var schoolClubs = new HashSet<SchoolClubPost>();
            //foreach(var post in schoolClubPosts)
            //{
            //    if(post.Missions == null || post.Missions.Count == 0)
            //    {
            //        continue;
            //    }

            //    foreach(var mission in post.Missions)
            //    {
            //        if(mission.Details.SchoolYear == selectedSchoolYear)
            //        {
            //            schoolClubs.Add(post);
            //        }
            //    }
            //}

            //model.SchoolClubs = schoolClubs.OrderByDescending(x => x.Points).ToList();

            //// Get Heroes count
            //var heroesCount = 0;
            //var missionsCount = 0;
            //var teamsCount = 0;
            //foreach (var post in schoolClubPosts)
            //{
            //    if (post.Participants.Count > 0)
            //    {
            //        heroesCount += post.Participants.Count;
            //    }

            //    if (post.Missions.Count > 0)
            //    {
            //        missionsCount += post.Missions.Count;
            //    }

            //    if (post.SchoolClubRegion != null)
            //    {
            //        teamsCount += 1;
            //    }
            //}

            //model.HeroesCount = heroesCount;
            //model.MissionsCount = missionsCount;
            //model.TeamsCount = teamsCount;

            //// Get missions
            //var missionsArchive = pages.First(p => p.TypeId == "MissionsArchive");
            //if (missionsArchive != null)
            //{
            //    var missionsArchiveId = missionsArchive.Id;
            //    var linkedMissionsPosts = await _api.Posts.GetAllAsync<LinkMissionPost>(missionsArchiveId);
            //    var lastEnteredLinkedMissions = linkedMissionsPosts.OrderByDescending(x => x.Published).Take(3);
            //    foreach (var post in lastEnteredLinkedMissions)
            //    {
            //        model.LinkedMissions.Add(post);
            //    }
            //}

            //// School years
            //if (missionsArchive != null)
            //{
            //    var missionsArchiveId = missionsArchive.Id;
            //    var linkedMissionsPosts = await _api.Posts.GetAllAsync<LinkMissionPost>(missionsArchiveId);
            //    var blogMissionPosts = await _api.Posts.GetAllAsync<BlogMissionPost>(missionsArchiveId);

            //    var schoolYears = new HashSet<String>();
            //    foreach (var post in blogMissionPosts)
            //    {
            //        schoolYears.Add(post.Details.SchoolYear);
            //    }

            //    model.SchoolYears = schoolYears.OrderByDescending(x => x.Contains(DateTime.Now.Year.ToString())).ToList();
            //}

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
            var model = await _loader.GetPostAsync<LinkMissionPost>(id, HttpContext.User, draft);

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
            var model = await _loader.GetPostAsync<BlogMissionPost>(id, HttpContext.User, draft);

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
            var model = await _loader.GetPageAsync<MissionsArchive>(id, HttpContext.User, draft);
            model.LinkMissionArchive = await _api.Archives.GetByIdAsync<LinkMissionPost>(id, page, category, tag, year, month);
            model.BlogMissionArchive = await _api.Archives.GetByIdAsync<BlogMissionPost>(id, page, category, tag, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the school club post with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("club")]
        public async Task<IActionResult> SchoolClubPost(Guid id, bool draft = false)
        {
            var model = await _loader.GetPostAsync<SchoolClubPost>(id, HttpContext.User, draft);

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
        [Route("clubs")]
        public async Task<IActionResult> SchoolClubsArchive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await _loader.GetPageAsync<SchoolClubArchive>(id, HttpContext.User, draft);
            model.SchoolClubsArchive = await _api.Archives.GetByIdAsync<SchoolClubPost>(id, page, category, tag, year, month);

            return View(model);
        }
    }
}