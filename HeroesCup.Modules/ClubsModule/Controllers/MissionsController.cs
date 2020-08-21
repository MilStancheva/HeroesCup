using ClubsModule.Exceptions;
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
    public class MissionsController : ManagerController
    {
        private readonly IMissionsService missionsService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;
        private readonly HeroesCup.Localization.ManagerLocalizer heroesCupLocalizer;

        public MissionsController(IMissionsService missionsService, IUserManager userManager, ManagerLocalizer heroesCupLocalizer)
        {
            this.missionsService = missionsService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
            this.heroesCupLocalizer = heroesCupLocalizer;
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
                ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be saved."]);
                if(model.Clubs == null)
                {
                    var validModel = await this.missionsService.GetMissionEditModelByIdAsync(model.Mission.Id);
                    return View("Edit", validModel);
                }

                return View("Edit", model);
            }

            try
            {
                var missionId = await this.missionsService.SaveMissionEditModelAsync(model);
                if (missionId != null && missionId != Guid.Empty)
                {
                    SuccessMessage(this.heroesCupLocalizer.Mission["The mission has been saved."]);
                    return RedirectToAction("Edit", new { id = missionId });
                }
            }
            catch (ExistingItemException)
            {
                ErrorMessage(this.heroesCupLocalizer.Mission["There is already a mission with the same title."]);
                return View("Edit", model);
            }
           

            ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be saved."]);
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
                SuccessMessage(this.heroesCupLocalizer.Mission["The mission has been published."]);
                return RedirectToAction("Edit", new { id = model.Mission.Id });
            }

            ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be published."], false);
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
                SuccessMessage(this.heroesCupLocalizer.Mission["The mission has been unpublished."]);
                return RedirectToAction("Edit", new { id = model.Mission.Id });
            }

            ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be unpublished."], false);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/mission/{id:Guid}")]
        [Authorize(Policy = Permissions.MissionsEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.missionsService.GetMissionEditModelByIdAsync(id, this.loggedInUserId);
            if (model == null)
            {
                ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be found."], false);
                return RedirectToAction("List");
            }

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
                ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be deleted."], false);
                return RedirectToAction("List");
            }

            SuccessMessage(this.heroesCupLocalizer.Mission["The mission has been deleted."]);
            return RedirectToAction("List");
        }

        [HttpPost]
        [Route("/manager/mission/pin")]
        [Authorize(Policy = Permissions.MissionsPublish)]
        public async Task<IActionResult> PinAsync(MissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var result = await this.missionsService.PinMissionEditModelAsync(model.Mission.Id);
            if (result)
            {
                SuccessMessage(this.heroesCupLocalizer.Mission["The mission has been pinned to home page."]);
                return RedirectToAction("Edit", new { id = model.Mission.Id });
            }

            ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be pinned to home page."], false);
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/manager/mission/unpin")]
        [Authorize(Policy = Permissions.MissionsPublish)]
        public async Task<IActionResult> UnpinAsync(MissionEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var result = await this.missionsService.UnpinMissionEditModelAsync(model.Mission.Id);
            if (result)
            {
                SuccessMessage(this.heroesCupLocalizer.Mission["The mission has been unpinned from home page."]);
                return RedirectToAction("Edit", new { id = model.Mission.Id });
            }

            ErrorMessage(this.heroesCupLocalizer.Mission["The mission could not be unpinned from home page."], false);
            return View("Edit", model);
        }
    }
}