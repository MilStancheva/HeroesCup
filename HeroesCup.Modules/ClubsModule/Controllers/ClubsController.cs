using ClubsModule.Models;
using ClubsModule.Services.Contracts;
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

        public ClubsController(IClubsService clubsService)
        {
            this.clubsService = clubsService;
        }

        [HttpGet]
        [Route("/manager/clubs")]
        [Authorize(Policy = Permissions.Clubs)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.clubsService.GetClubListModelAsync();
            return View(model);
        }

        [HttpGet]
        [Route("/manager/club")]
        [Authorize(Policy = Permissions.ClubsAdd)]
        public IActionResult Add()
        {
            var model = this.clubsService.CreateClubEditModel();
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/club/{id:Guid}")]
        [Authorize(Policy = Permissions.ClubsEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.clubsService.GetClubEditModelByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [Route("/manager/club/save")]
        [Authorize(Policy = Permissions.HeroesSave)]
        public async Task<IActionResult> SaveAsync(ClubEditModel model)
        {
            var result = await this.clubsService.SaveClubEditModel(model);
            if (result == true)
            {
                SuccessMessage("The club has been saved.");
                return RedirectToAction("Edit", new { id = model.Club.Id });
            }

            ErrorMessage("The club could not be saved.", false);
            return View("Edit", model);
        }
    }
}
