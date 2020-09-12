using HeroesCup.Web.Common;
using HeroesCup.Web.Models;
using HeroesCup.Web.Models.Events;
using HeroesCup.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Piranha;
using Piranha.AspNetCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Web.Controllers
{
    public class EventsController : Controller
    {
        private const string PageCountKey = "pageCount";
        private readonly IApi api;
        private readonly IModelLoader loader;
        private readonly IConfiguration configuration;
        private readonly IWebUtils webUtils;
        private int eventsCount;
        private IMetaDataProvider metaDataProvider;

        public EventsController(IApi api, IModelLoader loader, IConfiguration configuration, IWebUtils webUtils, IMetaDataProvider metaDataProvider)
        {
            this.api = api;
            this.loader = loader;
            this.configuration = configuration;
            int.TryParse(this.configuration["EventsCount"], out eventsCount);
            this.webUtils = webUtils;
            this.metaDataProvider = metaDataProvider;
        }

        // <summary>
        /// Gets the events blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="loadRequest">The optional load more events</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("events")]
        public async Task<IActionResult> EventsArchive(Guid id, bool loadRequest, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await loader.GetPageAsync<EventsArchive>(id, HttpContext.User, draft);
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

            var eventsArchive = await api.Archives.GetByIdAsync<EventPost>(id, page, category, tag, year, month);
            var posts = eventsArchive.Posts.OrderByDescending(p => p.Published.Value).Take((int)currentPageCount * eventsCount).ToList();

            model.Archive = eventsArchive;
            model.Archive.Posts = posts;
            model.SocialNetworksMetaData = this.metaDataProvider.getMetaData(HttpContext, model.Slug, model.Title);

            return View(model);
        }

        /// <summary>
        /// Gets the resource with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("event")]
        public async Task<IActionResult> EventPost(Guid id, bool draft = false)
        {
            var model = await loader.GetPostAsync<EventPost>(id, HttpContext.User, draft);
            var pages = await api.Pages.GetAllAsync();
            var eventsArchive = pages.FirstOrDefault(p => p.TypeId == "EventsArchive");
            if (eventsArchive != null)
            {
                var eventsArchiveId = eventsArchive.Id;
                var eventsPosts = await api.Posts.GetAllAsync<EventPost>(eventsArchiveId);
                int othersCount = 0;
                int.TryParse(configuration["EventsDetailsOthersCount"], out othersCount);
                model.OtherEvents = eventsPosts.Where(r => r.Id != model.Id).Take(othersCount).ToList();
                var currentUrlBase = webUtils.GetUrlBase(HttpContext);
                model.CurrentUrlBase = currentUrlBase;
                model.SiteCulture = await webUtils.GetCulture(this.api);
                var image = model.Hero != null && model.Hero.PrimaryImage.HasValue ? $"{currentUrlBase}{model.Hero.PrimaryImage.Media.PublicUrl.TrimStart(new char[] { '~' })}" : $"{currentUrlBase}/{this.configuration["FacebookDefaultImageUrl"]}";
                var url = $"{currentUrlBase}/{model.Category.Title}/{model.Slug}";
                model.SocialNetworksMetaData = this.metaDataProvider.getMetaData(HttpContext, model.Title, model.Title, url, image);
            }

            return View(model);
        }
    }
}