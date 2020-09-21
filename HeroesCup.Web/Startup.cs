using HeroesCup.Data;
using HeroesCup.Identity;
using HeroesCup.Modules.ClubsModule;
using HeroesCup.Web.Common;
using HeroesCup.Web.Common.Extensions;
using HeroesCup.Web.Models.Blocks;
using HeroesCup.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Piranha;
using Piranha.AspNetCore.Identity.MySQL;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.MySql;
using Piranha.Manager.Editor;
using Serilog;
using System;
using System.Text;

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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
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

            services.AddTransient<IHeroesCupIdentitySeed, IdentitySeed>();
            services.AddClubsModule();
            services.AddTransient<IPageInitializer, PageInitializer>();
            services.AddTransient<ILeaderboardService, LeaderboardService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IMissionsService, MissionsService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IWebUtils, WebUtils>();
            services.AddTransient<IVideoThumbnailParser, YouTubeVideoThumbnailParser>();
            services.AddTransient<IMetaDataProvider, MetaDataProvider>();
            services.AddControllersWithViews();

            Services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApi api)
        {
            if (!env.IsDevelopment())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseExceptionHandler("/error/500");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                {
                    //Re-execute the request so the user gets the error page
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/error/404";
                    await next();
                }
            });

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

            app.UseSerilogRequestLogging();
            app.UseUnhandledExceptionLogging();

            SeedDefaultPages();

            // Middleware setup
            app.UsePiranha(options =>
            {
                options.UseManager();
                options.UseTinyMCE();
                options.UseIdentity();
            });
            app.UsePiranhaStartPage();
            app.UseClubsModule();
            Piranha.App.MediaTypes.Images.Add(".svg", "image/svg+xml", false);

            ConfigurePiranhaEditor();

            RegisterPiranhaCustomBlocks();
        }

        private void SeedDefaultPages()
        {
            var dbSeed = Configuration["DbSeed"];
            if (dbSeed == "true")
            {
                var serviceProvider = Services.BuildServiceProvider();

                var identitySeed = serviceProvider.GetService<IHeroesCupIdentitySeed>();
                identitySeed.SeedIdentityAsync();
                var pagesInitializer = serviceProvider.GetService<IPageInitializer>();

                pagesInitializer.SeedMissionsPageAsync().Wait();
                pagesInitializer.SeedResourcesPageAsync().Wait();
                pagesInitializer.SeedEventsPageAsync().Wait();
                pagesInitializer.SeedAboutPageAsync().Wait();
                pagesInitializer.SeedStarPageAsync().Wait();
            }
        }

        private void ConfigurePiranhaEditor()
        {
            App.Modules.Get<Piranha.Manager.Module>().Styles.Add("~/css/manager-styles.css");
            EditorConfig.FromFile("editorconfig.json");
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add(Piranha.Manager.Editor.EditorScripts.MainScriptUrl);
            Piranha.Manager.Editor.EditorScripts.EditorScriptUrl = "~/scripts/tinymce-editor.js";
        }

        private void RegisterPiranhaCustomBlocks()
        {
            Piranha.App.Blocks.Register<EmbeddedVideoBlock>();
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("~/scripts/blocks/embedded-video-block.js");
        }
    }
}