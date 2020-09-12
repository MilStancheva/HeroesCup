using HeroesCup.Controllers;
using HeroesCup.Web.Common;
using HeroesCup.Web.Models;
using HeroesCup.Web.Models.Blocks;
using HeroesCup.Web.Models.Resources;
using HeroesCup.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Piranha;
using Piranha.AspNetCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Web.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly IApi api;
        private readonly IModelLoader loader;
        private readonly IConfiguration configuration;
        private readonly IWebUtils webUtils;
        private readonly IVideoThumbnailParser videoThumbnailParser;
        private readonly IMetaDataProvider metaDataProvider;

        public ResourcesController(IApi api, 
            IModelLoader loader, 
            IConfiguration configuration, 
            IWebUtils webUtils, 
            IVideoThumbnailParser videoThumbnailParser,
            IMetaDataProvider metaDataProvider)
        {
            this.api = api;
            this.loader = loader;
            this.configuration = configuration;
            this.webUtils = webUtils;
            this.videoThumbnailParser = videoThumbnailParser;
            this.metaDataProvider = metaDataProvider;
        }

        // <summary>
        /// Gets the resources blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("resources")]
        public async Task<IActionResult> ResourcesArchive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null, bool draft = false)
        {
            var model = await loader.GetPageAsync<ResourcesArchive>(id, HttpContext.User, draft);
            model.Archive = await api.Archives.GetByIdAsync<ResourcePost>(id, page, category, tag, year, month);
            model.SocialNetworksMetaData = this.metaDataProvider.getMetaData(HttpContext, model.Slug, model.Title);

            return View(model);
        }

        /// <summary>
        /// Gets the resource with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="draft">If a draft is requested</param>
        [Route("resource")]
        public async Task<IActionResult> ResourcePost(Guid id, bool draft = false)
        {
            var model = await loader.GetPostAsync<ResourcePost>(id, HttpContext.User, draft);
            var pages = await api.Pages.GetAllAsync();
            var currentUrlBase = webUtils.GetUrlBase(HttpContext);
            model.CurrentUrlBase = currentUrlBase;
            model.SiteCulture = await webUtils.GetCulture(this.api);
            var resourcesArchive = pages.FirstOrDefault(p => p.TypeId == "ResourcesArchive");
            if (resourcesArchive != null)
            {
                var resourcesArchiveId = resourcesArchive.Id;
                var resourcesPosts = await api.Posts.GetAllAsync<ResourcePost>(resourcesArchiveId);
                int othersCount = 0;
                int.TryParse(configuration["ResourcesDetailsOthersCount"], out othersCount);
                model.OtherResources = resourcesPosts.Where(r => r.Id != model.Id).Take(othersCount).ToList();
                
                var image = model.Hero != null && model.Hero.PrimaryImage.HasValue ? $"{currentUrlBase}{model.Hero.PrimaryImage.Media.PublicUrl.TrimStart(new char[] { '~'})}" : $"{currentUrlBase}/{this.configuration["FacebookDefaultImageUrl"]}";
                var url = $"{currentUrlBase}/{model.Category.Title}/{model.Slug}";
                model.SocialNetworksMetaData = this.metaDataProvider.getMetaData(HttpContext, model.Title, model.Title, url, image);

                if (model.Type.Value == ResourcePostType.VIDEO)
                {
                    var firstEmbedVideoBlock = model.Blocks.Where(b => b.Type == "HeroesCup.Web.Models.Blocks.EmbeddedVideoBlock").FirstOrDefault() as EmbeddedVideoBlock;
                    if (firstEmbedVideoBlock != null)
                    {
                        var videoUrl = firstEmbedVideoBlock.Source;
                        model.VideoThumbnail = videoThumbnailParser.ParseDefaultThubnailUrl(videoUrl);
                        model.VideoUrl = videoUrl;
                        model.SocialNetworksMetaData.VideoUrl = videoUrl;
                        model.SocialNetworksMetaData.VideoType = this.configuration["FacebookDefaultVideoType"];
                        model.SocialNetworksMetaData.Image = model.VideoThumbnail;
                    }                 
                }
            }       

            return View(model);
        }
    }
}