using HeroesCup.Models;
using Microsoft.Extensions.Configuration;
using Piranha;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Services
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
                newStartPage.Title = "Home";
                newStartPage.Slug = "";
                newStartPage.MetaKeywords = "За купата, home";
                newStartPage.MetaDescription = "За купата начална страница HeroesCu home page";
                newStartPage.NavigationTitle = "Home";
                newStartPage.Published = DateTime.Now;
                await _api.Pages.SaveAsync<StartPage>(newStartPage);
            }
        }

        public async Task SeedAboutPageAsync()
        {
            var site = await _api.Sites.GetDefaultAsync();
            var pages = await _api.Pages.GetAllAsync();
            var aboutPageTitle = this._configuration["AboutPageTitle"];
            var aboutPage = pages.ToList().FirstOrDefault(p => p.Title == aboutPageTitle);
            if (aboutPage == null)
            {
                var newAboutPage = await StandardPage.CreateAsync(_api);
                newAboutPage.Id = Guid.NewGuid();
                newAboutPage.SiteId = site.Id;
                newAboutPage.Title = "About";
                newAboutPage.Slug = "about";
                newAboutPage.MetaKeywords = "За купата, about";
                newAboutPage.MetaDescription = "За купата";
                newAboutPage.NavigationTitle = "За купата";
                newAboutPage.Published = DateTime.Now;
                await _api.Pages.SaveAsync<StandardPage>(newAboutPage);
            }
        }
    }
}
