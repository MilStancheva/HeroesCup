using HeroesCup.Data;
using HeroesCup.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha;
using Piranha.AspNetCore.Identity.MySQL;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.MySql;
using Piranha.Manager.Editor;
using HeroesCup.Modules.ClubsModule;
using System;

namespace HeroesCup
{
    public class Startup
    {
        /// <summary>
        /// The application config.
        /// </summary>
        public IConfiguration Configuration { get; set; }

        private IServiceCollection Services { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="configuration">The current configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
            });
            var connectionString = Configuration.GetSection("HEROESCUP_CONNECTIONSTRING").Value;

            services.AddDbContext<HeroesCupDbContext>(opt =>
                opt.UseMySql(connectionString));

            // Service setup
            services.AddPiranha(options =>
            {
                options.UseFileStorage();
                options.UseImageSharp();
                options.UseManager();
                options.UseTinyMCE();
                options.UseMemoryCache();
                options.UseEF<MySqlDb>(db =>
                    db.UseMySql(connectionString));
                options.UseIdentityWithSeed<IdentityMySQLDb>(db =>
                    db.UseMySql(connectionString));
            });
            services.AddClubsModule();
            services.AddTransient<IPageInitializer, PageInitializer>();

            services.AddControllersWithViews();

            Services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApi api)
        {

            app.UseSession();
            // Initialize Piranha
            App.Init(api);

            // Configure cache level
            App.CacheLevel = Piranha.Cache.CacheLevel.Basic;

            // Build content types
            new ContentTypeBuilder(api)
                .AddAssembly(typeof(Startup).Assembly)
                .Build()
                .DeleteOrphans();

            // Configure Tiny MCE
            EditorConfig.FromFile("editorconfig.json");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDefaultPages();
            }
            // Middleware setup
            app.UsePiranha(options =>
            {
                options.UseManager();
                options.UseTinyMCE();
                options.UseIdentity();
            });
            app.UsePiranhaStartPage();
            app.UseClubsModule();

        }

        private void SeedDefaultPages()
        {
            var dbSeed = Configuration["DbSeed"];
            if (dbSeed == "true")
            {
                var serviceProvider = Services.BuildServiceProvider();
                var pagesInitializer = serviceProvider.GetService<IPageInitializer>();

                pagesInitializer.SeedResourcesPageAsync().Wait();
                pagesInitializer.SeedEventsPageAsync().Wait();
                pagesInitializer.SeedAboutPageAsync().Wait();
                pagesInitializer.SeedStarPageAsync().Wait();
            }
        }
    }
}