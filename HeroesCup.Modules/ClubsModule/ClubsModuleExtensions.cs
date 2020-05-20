using ClubsModule;
using ClubsModule.Security;
using ClubsModule.Services;
using ClubsModule.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Piranha;
using Piranha.Manager;

namespace HeroesCup.Modules.ClubsModule
{
    public static class ClubsModuleExtensions
    {
        public static IServiceCollection AddClubsModule(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            App.Modules.Register<Module>();
            services.AddTransient<IHeroesService, HeroesService>();
            services.AddTransient<IClubsService, ClubsService>();
            services.AddTransient<IImagesService, ImagesService>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IMissionsService, MissionsService>();
            services.AddTransient<IPointsService, PointsService>();

            services.AddAuthorization(options =>
            {
                // Clubs policies
                options.AddPolicy(Permissions.Clubs, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Clubs, Permissions.Clubs);
                });

                options.AddPolicy(Permissions.ClubsAdd, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Clubs, Permissions.Clubs);
                    policy.RequireClaim(Permissions.ClubsAdd, Permissions.ClubsAdd);
                });

                options.AddPolicy(Permissions.ClubsDelete, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Clubs, Permissions.Clubs);
                    policy.RequireClaim(Permissions.ClubsDelete, Permissions.ClubsDelete);
                });

                options.AddPolicy(Permissions.ClubsEdit, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Clubs, Permissions.Clubs);
                    policy.RequireClaim(Permissions.ClubsEdit, Permissions.ClubsEdit);
                });

                options.AddPolicy(Permissions.ClubsSave, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Clubs, Permissions.Clubs);
                    policy.RequireClaim(Permissions.ClubsSave, Permissions.ClubsSave);
                });

                // Heroes policies
                options.AddPolicy(Permissions.Heroes, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Heroes, Permissions.Heroes);
                });

                options.AddPolicy(Permissions.HeroesAdd, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Heroes, Permissions.Heroes);
                    policy.RequireClaim(Permissions.HeroesAdd, Permissions.HeroesAdd);
                });

                options.AddPolicy(Permissions.HeroesDelete, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Heroes, Permissions.Heroes);
                    policy.RequireClaim(Permissions.HeroesDelete, Permissions.HeroesDelete);
                });

                options.AddPolicy(Permissions.HeroesEdit, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Heroes, Permissions.Heroes);
                    policy.RequireClaim(Permissions.HeroesEdit, Permissions.HeroesEdit);
                });

                options.AddPolicy(Permissions.HeroesSave, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Heroes, Permissions.Heroes);
                    policy.RequireClaim(Permissions.HeroesSave, Permissions.HeroesSave);
                });

                options.AddPolicy(Permissions.HeroesAddCoordinator, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Heroes, Permissions.Heroes);
                    policy.RequireClaim(Permissions.HeroesAddCoordinator, Permissions.HeroesAddCoordinator);
                });

                // Missions policies
                options.AddPolicy(Permissions.Missions, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                });

                options.AddPolicy(Permissions.MissionsAdd, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                    policy.RequireClaim(Permissions.MissionsAdd, Permissions.MissionsAdd);
                });

                options.AddPolicy(Permissions.MissionsDelete, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                    policy.RequireClaim(Permissions.MissionsDelete, Permissions.MissionsDelete);
                });

                options.AddPolicy(Permissions.MissionsEdit, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                    policy.RequireClaim(Permissions.MissionsEdit, Permissions.MissionsEdit);
                });

                options.AddPolicy(Permissions.MissionsSave, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                    policy.RequireClaim(Permissions.MissionsSave, Permissions.MissionsSave);
                });

                options.AddPolicy(Permissions.MissionsStars, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                    policy.RequireClaim(Permissions.MissionsStars, Permissions.MissionsStars);
                });

                options.AddPolicy(Permissions.MissionsPublish, policy =>
                {
                    policy.RequireClaim(Permission.Admin, Permission.Admin);
                    policy.RequireClaim(Permissions.Missions, Permissions.Missions);
                    policy.RequireClaim(Permissions.MissionsPublish, Permissions.MissionsPublish);
                });
            });

            return services;
        }
        public static void MapClubsModule(this IEndpointRouteBuilder builder)
        {
            builder.MapRazorPages();
        }
        public static IApplicationBuilder UseClubsModule(this IApplicationBuilder builder)
        {
            // Manager resources
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapClubsModule();
            });

            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("~/manager/clubsmodule/js/components/blocks/clubs.js");
            App.Modules.Get<Piranha.Manager.Module>().Styles.Add("~/manager/clubsmodule/css/styles.css");

            App.Modules.Get<Piranha.Manager.Module>().Styles.Add("https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css");
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("https://unpkg.com/gijgo@1.9.13/js/gijgo.min.js");
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("https://unpkg.com/gijgo@1.9.13/js/messages/messages.bg-bg.js");
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("~/manager/clubsmodule/js/mission-datetimepicker.js");
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("~/manager/clubsmodule/js/editor.js");
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("~/manager/clubsmodule/js/mission-multiselect.js");
            //App.Modules.Get<Piranha.Manager.Module>().Styles.Add("http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css");
            //App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.14/dist/js/bootstrap-select.min.js");

            return builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(ClubsModuleExtensions).Assembly, "ClubsModule.assets.src"),
                RequestPath = "/manager/clubsmodule"
            });
        }
    }
}