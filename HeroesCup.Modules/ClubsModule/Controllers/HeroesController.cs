using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class HeroesController : ManagerController
    {
        private readonly IHeroesService heroesService;
        private HttpContext context;
        private Guid loggedInUserId;

        public HeroesController(IHeroesService heroesService, IHttpContextAccessor httpAccess)
        {
            this.heroesService = heroesService;
            this.context = httpAccess.HttpContext;
            this.loggedInUserId = new Guid(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        [Route("/manager/heroes")]
        [Authorize(Policy = Permissions.Heroes)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.heroesService.GetHeroListModelAsync(this.loggedInUserId);
            return View(model);
        }

        [HttpGet]
        [Route("/manager/hero")]
        [Authorize(Policy = Permissions.HeroesAdd)]
        public async Task<IActionResult> Add()
        {
            var model = await this.heroesService.CreateHeroEditModel(this.loggedInUserId);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/hero/{id:Guid}")]
        [Authorize(Policy = Permissions.HeroesEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.heroesService.GetHeroEditModelByIdAsync(id, this.loggedInUserId);
            return View(model);
        }

        [HttpPost]
        [Route("/manager/hero/save")]
        [Authorize(Policy = Permissions.HeroesSave)]
        public async Task<IActionResult> SaveAsync(HeroEditModel model)
        {
            var heroId = await this.heroesService.SaveHeroEditModel(model);
            if (heroId != null)
            {
                SuccessMessage("The hero has been saved.");
                return RedirectToAction("Edit", new { id = heroId });
            }

            ErrorMessage("The hero could not be saved.", false);
            return View("Edit", model);
        }

    }
}
