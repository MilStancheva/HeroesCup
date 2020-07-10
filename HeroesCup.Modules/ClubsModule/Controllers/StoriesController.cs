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
    public class StoriesController : ManagerController
    {
        private readonly IStoriesService storiesService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;
        private readonly HeroesCup.Localization.ManagerLocalizer heroesCupLocalizer;

        public StoriesController(IStoriesService stroiesService, IUserManager userManager, ManagerLocalizer heroesCupLocalizer)
        {
            this.storiesService = stroiesService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
            this.heroesCupLocalizer = heroesCupLocalizer;
        }

        [HttpGet]
        [Route("/manager/stories")]
        [Authorize(Policy = Permissions.Stories)]
        public async Task<IActionResult> ListAsync()
        {
            var model = await this.storiesService.GetStoryListModelAsync(this.loggedInUserId);
            return View(model);
        }

        [HttpGet]
        [Route("/manager/story")]
        [Authorize(Policy = Permissions.StoriesAdd)]
        public async Task<IActionResult> Add()
        {
            var model = await this.storiesService.CreateStoryEditModelAsync(this.loggedInUserId);
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/manager/story/save")]
        [Authorize(Policy = Permissions.StoriesSave)]
        public async Task<IActionResult> SaveAsync(StoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage(this.heroesCupLocalizer.Story["The story could not be saved."]);
                return RedirectToAction("Edit", new { id = model.Story.Id });
            }

            var storyId = await this.storiesService.SaveStoryEditModelAsync(model);
            if (storyId != null && storyId != Guid.Empty)
            {
                SuccessMessage(this.heroesCupLocalizer.Story["The story has been saved."]);
                return RedirectToAction("Edit", new { id = storyId });
            }

            ErrorMessage(this.heroesCupLocalizer.Story["The story could not be saved."]);
            return RedirectToAction("Edit", new { id = model.Story.Id });
        }

        [HttpGet]
        [Route("/manager/story/{id:Guid}")]
        [Authorize(Policy = Permissions.StoriesEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.storiesService.GetStoryEditModelByIdAsync(id, this.loggedInUserId);
            if (model == null)
            {
                ErrorMessage(this.heroesCupLocalizer.Story["The story could not be found."], false);
                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpGet]
        [Route("/manager/story/delete")]
        [Authorize(Policy = Permissions.StoriesDelete)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await this.storiesService.DeleteAsync(id);
            if (!result)
            {
                ErrorMessage(this.heroesCupLocalizer.Story["The story could not be deleted."], false);
                return RedirectToAction("List");
            }

            SuccessMessage(this.heroesCupLocalizer.Story["The story has been deleted."]);
            return RedirectToAction("List");
        }

        [HttpPost]
        [Route("/manager/story/publish")]
        [Authorize(Policy = Permissions.StoriesPublish)]
        public async Task<IActionResult> PublishAsync(StoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var result = await this.storiesService.PublishStoryEditModelAsync(model.Story.Id);
            if (result)
            {
                SuccessMessage(this.heroesCupLocalizer.Story["The story has been published."]);
                return RedirectToAction("Edit", new { id = model.Story.Id });
            }

            ErrorMessage(this.heroesCupLocalizer.Story["The story could not be published."], false);
            return View("Edit", model);
        }

        [HttpPost]
        [Route("/manager/story/unpublish")]
        [Authorize(Policy = Permissions.StoriesPublish)]
        public async Task<IActionResult> UnpublishAsync(StoryEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var result = await this.storiesService.UnpublishStoryEditModelAsync(model.Story.Id);
            if (result)
            {
                SuccessMessage(this.heroesCupLocalizer.Story["The story has been unpublished."]);
                return RedirectToAction("Edit", new { id = model.Story.Id });
            }

            ErrorMessage(this.heroesCupLocalizer.Story["The story could not be unpublished."], false);
            return View("Edit", model);
        }
    }
}