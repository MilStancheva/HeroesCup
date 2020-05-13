using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class HeroesController : ManagerController
    {
        private readonly IHeroesService heroesService;

        public HeroesController(IHeroesService heroesService)
        {
            this.heroesService = heroesService;
        }

        [HttpGet]
        [Route("/manager/heroes")]
        [Authorize(Policy = Permissions.Heroes)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.heroesService.GetHeroListModelAsync();
            return View(model);
        }

        [HttpGet]
        [Route("/manager/hero")]
        [Authorize(Policy = Permissions.HeroesAdd)]
        public IActionResult Add()
        {
            var model = this.heroesService.CreateHeroEditModel();
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/hero/{id:Guid}")]
        [Authorize(Policy = Permissions.HeroesEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.heroesService.GetHeroEditModelByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [Route("/manager/hero/save")]
        [Authorize(Policy = Permissions.HeroesSave)]
        public async Task<IActionResult> SaveAsync(HeroEditModel model)
        {
            var result = await this.heroesService.SaveHeroEditModel(model);
            if (result == true)
            {
                SuccessMessage("The hero has been saved.");
                return RedirectToAction("Edit", new { id = model.Hero.Id });
            }

            ErrorMessage("The hero could not be saved.", false);
            return View("Edit", model);
        }

    }
}
