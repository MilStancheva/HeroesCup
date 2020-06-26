using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class MissionIdeasController : ManagerController
    {
        private readonly IMissionIdeasService missionIdeasService;
        private readonly HeroesCup.Localization.ManagerLocalizer heroesCupLocalizer;

        public MissionIdeasController(IMissionIdeasService missionIdeasService, ManagerLocalizer heroesCupLocalizer)
        {
            this.missionIdeasService = missionIdeasService;
            this.heroesCupLocalizer = heroesCupLocalizer;
        }

        [HttpGet]
        [Route("/manager/missionideas")]
        [Authorize(Policy = Permissions.MissionIdeas)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.missionIdeasService.GetMissionIdeasListModelAsync();
            return View(model);
        }

        [HttpGet]
        [Route("/manager/missionidea")]
        [Authorize(Policy = Permissions.MissionIdeasAdd)]
        public IActionResult Add()
        {
            var model = this.missionIdeasService.CreateMissionIdeaEditModel();
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/missionidea/{id:Guid}")]
        [Authorize(Policy = Permissions.MissionIdeasEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.missionIdeasService.GetMissionIdeaEditModelByIdAsync(id);
            if (model == null)
            {
                ErrorMessage(this.heroesCupLocalizer.Mission["The mission idea could not be found."], false);
                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpPost]
        [Route("/manager/missionidea/save")]
        [Authorize(Policy = Permissions.MissionIdeasSave)]
        public async Task<IActionResult> SaveAsync(MissionIdeaEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage(this.heroesCupLocalizer.Mission["The mission idea could not be saved."], false);
                return View("Edit", model);
            }

            var missionId = await this.missionIdeasService.SaveMissionIdeaEditModelAsync(model);
            if (missionId != null && missionId != Guid.Empty)
            {
                SuccessMessage(this.heroesCupLocalizer.Mission["The mission idea has been saved."]);
                return RedirectToAction("Edit", new { id = missionId });
            }

            ErrorMessage(this.heroesCupLocalizer.Mission["The mission idea could not be saved."], false);
            return View("Edit", model);
        }
    }
}
