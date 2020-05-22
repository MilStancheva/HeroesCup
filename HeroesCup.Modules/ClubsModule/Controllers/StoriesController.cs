using ClubsModule.Models;
using ClubsModule.Security;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Piranha.Manager.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClubsModule.Controllers
{
    public class StoriesController : ManagerController
    {
        private readonly IStoriesService storiesService;
        private readonly IUserManager userManager;
        private Guid? loggedInUserId;

        public StoriesController(IStoriesService stroiesService, IUserManager userManager)
        {
            this.storiesService = stroiesService;
            this.userManager = userManager;
            this.loggedInUserId = this.userManager.GetCurrentUserId();
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
                return View("Edit", model);
            }

            var storyId = await this.storiesService.SaveStoryEditModelAsync(model);
            if (storyId != null && storyId != Guid.Empty)
            {
                SuccessMessage("The story has been saved.");
                return RedirectToAction("Edit", new { id = storyId });
            }

            ErrorMessage("The story could not be saved.", false);
            return View("Edit", model);
        }

        [HttpGet]
        [Route("/manager/story/{id:Guid}")]
        [Authorize(Policy = Permissions.StoriesEdit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var model = await this.storiesService.GetStoryEditModelByIdAsync(id, this.loggedInUserId);
            if (model == null)
            {
                ErrorMessage("The story could not be found.", false);
                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}
