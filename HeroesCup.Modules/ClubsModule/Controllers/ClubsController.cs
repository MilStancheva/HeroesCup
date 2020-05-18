using ClubsModule.Models;
using ClubsModule.Security;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class ClubsController : ManagerController
    {
        private readonly IClubsService clubsService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;

        public ClubsController(IClubsService clubsService, IUserManager userManager)
        {
            this.clubsService = clubsService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
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
            var model = await this.clubsService.CreateClubEditModel(this.loggedInUserId);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/club/{id:Guid}")]
        [Authorize(Policy = Permissions.ClubsEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.clubsService.GetClubEditModelByIdAsync(id, this.loggedInUserId);
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

            var clubId = await this.clubsService.SaveClubEditModel(model);
            if (clubId != null && clubId != Guid.Empty)
            {
                SuccessMessage("The club has been saved.");
                return RedirectToAction("Edit", new { id = clubId });
            }

            ErrorMessage("The club could not be saved.", false);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/club/delete")]
        [Authorize(Policy = Permissions.ClubsDelete)]
        public  async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.clubsService.DeleteClub(id);
            if (!result)
            {
                ErrorMessage("The club could not be deleted.", false);
                return RedirectToAction("List");
            }

            SuccessMessage("The club has been deleted.");
            return RedirectToAction("List");
        }
    }
}
