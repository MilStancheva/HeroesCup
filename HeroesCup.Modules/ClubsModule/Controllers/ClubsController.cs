using ClubsModule.Models;
using ClubsModule.Security;
using ClubsModule.Services.Contracts;
using HeroesCup.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class ClubsController : ManagerController
    {
        private readonly IClubsService clubsService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;
        private readonly HeroesCup.Localization.ManagerLocalizer heroesCupLocalizer;

        public ClubsController(IClubsService clubsService, IUserManager userManager, ManagerLocalizer heroesCupLocalizer)
        {
            this.clubsService = clubsService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
            this.heroesCupLocalizer = heroesCupLocalizer;
        }

        [HttpGet]
        [Route("/manager/clubs")]
        [Authorize(Policy = Permissions.Clubs)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.clubsService.GetClubListModelAsync(this.loggedInUserId);
            return View(model);
        }

        [HttpGet]
        [Route("/manager/club")]
        [Authorize(Policy = Permissions.ClubsAdd)]
        public async Task<IActionResult> Add()
        {
            var model = await this.clubsService.CreateClubEditModelAsync(this.loggedInUserId);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/club/{id:Guid}")]
        [Authorize(Policy = Permissions.ClubsEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.clubsService.GetClubEditModelByIdAsync(id, this.loggedInUserId);
            if (model == null)
            {
                ErrorMessage(this.heroesCupLocalizer.Club["The club could not be found."], false);
                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpPost]
        [Route("/manager/club/save")]
        [Authorize(Policy = Permissions.ClubsSave)]
        public async Task<IActionResult> SaveAsync(ClubEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var clubId = await this.clubsService.SaveClubEditModelAsync(model);
            if (clubId != null && clubId != Guid.Empty)
            {
                SuccessMessage(this.heroesCupLocalizer.Club["The club has been saved."]);
                return RedirectToAction("Edit", new { id = clubId });
            }

            ErrorMessage(this.heroesCupLocalizer.Club["The club could not be saved."], false);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/club/delete")]
        [Authorize(Policy = Permissions.ClubsDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.clubsService.DeleteAsync(id);
            if (!result)
            {
                ErrorMessage(this.heroesCupLocalizer.Club["The club could not be deleted."], false);
                return RedirectToAction("List");
            }

            SuccessMessage(this.heroesCupLocalizer.Club["The club has been deleted."]);
            return RedirectToAction("List");
        }
    }
}