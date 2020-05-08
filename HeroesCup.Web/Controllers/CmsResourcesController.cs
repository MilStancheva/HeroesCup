using HeroesCup.Web.Models;
using HeroesCup.Web.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.AspNetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Web.Controllers
{
    public class CmsResourcesController : Controller
    {
        private readonly IApi _api;
        private readonly IModelLoader _loader;

        public CmsResourcesController(IApi api, IModelLoader loader)
        {
            _api = api;
            _loader = loader;
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
            var model = await _loader.GetPageAsync<ResourcesArchive>(id, HttpContext.User, draft);
            model.Archive = await _api.Archives.GetByIdAsync<ResourcePost>(id, page, category, tag, year, month);

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
            var model = await _loader.GetPostAsync<ResourcePost>(id, HttpContext.User, draft);

            return View(model);
        }
    }
}
