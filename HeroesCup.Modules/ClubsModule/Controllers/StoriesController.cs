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
    }
}
