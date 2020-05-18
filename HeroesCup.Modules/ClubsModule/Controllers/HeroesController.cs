using ClubsModule.Models;
using ClubsModule.Security;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class HeroesController : ManagerController
    {
        private readonly IHeroesService heroesService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;

        public HeroesController(IHeroesService heroesService, IUserManager userManager)
        {
            this.heroesService = heroesService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
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
            var model = await this.heroesService.CreateHeroEditModelAsync(this.loggedInUserId);
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
            var heroId = await this.heroesService.SaveHeroEditModelAsync(model);
            if (heroId != null && heroId != Guid.Empty)
            {
                SuccessMessage("The hero has been saved.");
                return RedirectToAction("Edit", new { id = heroId });
            }

            ErrorMessage("The hero could not be saved.", false);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/hero/delete")]
        [Authorize(Policy = Permissions.ClubsDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.heroesService.DeleteAsync(id);
            if (!result)
            {
                ErrorMessage("The hero could not be deleted.", false);
                return RedirectToAction("List");
            }

            SuccessMessage("The hero has been deleted.");
            return RedirectToAction("List");
        }

    }
}