﻿using ClubsModule.Models;
using ClubsModule.Security;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost]
        [Route("/manager/mission/save")]
        [Authorize(Policy = Permissions.MissionsSave)]
        public async Task<IActionResult> SaveAsync(MissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage("The mission could not be saved.", false);
                return View("Edit", model);
            }

            var missionId = await this.missionsService.SaveMissionEditModelAsync(model);
            if (missionId != null && missionId != Guid.Empty)
            {
                SuccessMessage("The mission has been saved.");
                return RedirectToAction("Edit", new { id = missionId });
            }

            ErrorMessage("The mission could not be saved.", false);
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/manager/mission/publish")]
        [Authorize(Policy = Permissions.MissionsPublish)]
        public async Task<IActionResult> PublishAsync(MissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var result = await this.missionsService.PublishMissionEditModelAsync(model.Mission.Id);
            if (result)
            {
                SuccessMessage("The mission has been published.");
                return RedirectToAction("Edit", new { id = model.Mission.Id });
            }

            ErrorMessage("The mission could not be published.", false);
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/manager/mission/unpublish")]
        [Authorize(Policy = Permissions.MissionsPublish)]
        public async Task<IActionResult> UnpublishAsync(MissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var result = await this.missionsService.UnpublishMissionEditModelAsync(model.Mission.Id);
            if (result)
            {
                SuccessMessage("The mission has been unpublished.");
                return RedirectToAction("Edit", new { id = model.Mission.Id });
            }

            ErrorMessage("The mission could not be unpublished.", false);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/mission/{id:Guid}")]
        [Authorize(Policy = Permissions.MissionsEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.missionsService.GetMissionEditModelByIdAsync(id, this.loggedInUserId);
            return View(model);
        }

        [HttpGet]
        [Route("/manager/mission/delete")]
        [Authorize(Policy = Permissions.MissionsDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.missionsService.DeleteAsync(id);
            if (!result)
            {
                ErrorMessage("The mission could not be deleted.", false);
                return RedirectToAction("List");
            }

            SuccessMessage("The mission has been deleted.");
            return RedirectToAction("List");
        }
    }
}