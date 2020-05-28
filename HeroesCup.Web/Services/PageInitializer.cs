using HeroesCup.Models;
using HeroesCup.Web.Models.Events;
using HeroesCup.Web.Models.Resources;
using Microsoft.Extensions.Configuration;
using Piranha;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public class PageInitializer : IPageInitializer
    {
        private readonly IApi _api;
        private readonly IConfiguration _configuration;

        public PageInitializer(IApi api, IConfiguration configuration)
        {
            this._api = api;
            this._configuration = configuration;
        }

        public async Task SeedStarPageAsync()
        {
            var site = await _api.Sites.GetDefaultAsync();
            var pages = await _api.Pages.GetAllAsync();
            var startPage = pages.ToList().FirstOrDefault(p => p.TypeId == "StartPage");
            if (startPage == null)
            {
                var newStartPage = await StartPage.CreateAsync(_api);
                newStartPage.Id = Guid.NewGuid();
                newStartPage.SiteId = site.Id;
                newStartPage.Title = this._configuration["StartPagePageSettings:Title"];
                newStartPage.Slug = this._configuration["StartPagePageSettings:Slug"];
                newStartPage.MetaKeywords = this._configuration["StartPagePageSettings:MetaKeywords"];
                newStartPage.MetaDescription = this._configuration["StartPagePageSettings:MetaDescription"];
                newStartPage.NavigationTitle = this._configuration["StartPagePageSettings:NavigationTitle"];
                newStartPage.Published = DateTime.Now;
                await _api.Pages.SaveAsync<StartPage>(newStartPage);
            }
        }

        public async Task SeedAboutPageAsync()
        {
            var site = await _api.Sites.GetDefaultAsync();
            var pages = await _api.Pages.GetAllAsync();
            var aboutPageTitle = this._configuration["AboutPageSettings:Title"];
            var aboutPage = pages.ToList().FirstOrDefault(p => p.Title == aboutPageTitle);
            if (aboutPage == null)
            {
                var newAboutPage = await StandardPage.CreateAsync(_api);
                newAboutPage.Id = Guid.NewGuid();
                newAboutPage.SiteId = site.Id;
                newAboutPage.Title = aboutPageTitle;
                newAboutPage.Slug = this._configuration["AboutPageSettings:Slug"];
                newAboutPage.MetaKeywords = this._configuration["AboutPageSettings:MetaKeywords"];
                newAboutPage.MetaDescription = this._configuration["AboutPageSettings:MetaDescription"];
                newAboutPage.NavigationTitle = this._configuration["AboutPageSettings:NavigationTitle"]; ;
                newAboutPage.Published = DateTime.Now;
                await _api.Pages.SaveAsync<StandardPage>(newAboutPage);
            }
        }

        public async Task SeedResourcesPageAsync()
        {
            var site = await _api.Sites.GetDefaultAsync();
            var pages = await _api.Pages.GetAllAsync();
            var resourcesPage = pages.ToList().FirstOrDefault(p => p.TypeId == "ResourcesArchive");
            if (resourcesPage == null)
            {
                var newResourcesPage = await ResourcesArchive.CreateAsync(_api);
                newResourcesPage.Id = Guid.NewGuid();
                newResourcesPage.SiteId = site.Id;
                newResourcesPage.Title = this._configuration["ResourcesPageSettings:Title"];
                newResourcesPage.Slug = this._configuration["ResourcesPageSettings:Slug"];
                newResourcesPage.MetaKeywords = this._configuration["ResourcesPageSettings:MetaKeywords"];
                newResourcesPage.MetaDescription = this._configuration["ResourcesPageSettings:MetaKeywords"];
                newResourcesPage.NavigationTitle = this._configuration["ResourcesPageSettings:NavigationTitle"];
                newResourcesPage.Published = DateTime.Now;
                await _api.Pages.SaveAsync<ResourcesArchive>(newResourcesPage);
            }
        }

        public async Task SeedEventsPageAsync()
        {
            var site = await _api.Sites.GetDefaultAsync();
            var pages = await _api.Pages.GetAllAsync();
            var eventsPage = pages.ToList().FirstOrDefault(p => p.TypeId == "EventsArchive");
            if (eventsPage == null)
            {
                var newEventsPage = await EventsArchive.CreateAsync(_api);
                newEventsPage.Id = Guid.NewGuid();
                newEventsPage.SiteId = site.Id;
                newEventsPage.Title = this._configuration["EventsPageSettings:Title"];
                newEventsPage.Slug = this._configuration["EventsPageSettings:Slug"];
                newEventsPage.MetaKeywords = this._configuration["EventsPageSettings:MetaKeywords"];
                newEventsPage.MetaDescription = this._configuration["EventsPageSettings:MetaKeywords"];
                newEventsPage.NavigationTitle = this._configuration["EventsPageSettings:NavigationTitle"];
                newEventsPage.Published = DateTime.Now;
                await _api.Pages.SaveAsync<EventsArchive>(newEventsPage);
            }
        }
    }
}