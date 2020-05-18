using ClubsModule.Security;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class MissionsController : ManagerController
    {
        private readonly IMissionsService missionsService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;

        public MissionsController(IMissionsService missionsService, IUserManager userManager)
        {
            this.missionsService = missionsService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
        }

        [HttpGet]
        [Route("/manager/missions")]
        [Authorize(Policy = Permissions.Missions)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.missionsService.GetMissionListModelAsync(this.loggedInUserId);
            return View(model);
        }

        [HttpGet]
        [Route("/manager/mission")]
        [Authorize(Policy = Permissions.MissionsAdd)]
        public async Task<IActionResult> Add()
        {
            var model = await this.missionsService.CreateMissionEditModelAsync(this.loggedInUserId);
            return View("Edit", model);
        }
    }
}
