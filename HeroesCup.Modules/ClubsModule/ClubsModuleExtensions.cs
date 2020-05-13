using ClubsModule;
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
            App.Modules.Register<Module>();
            services.AddTransient<IHeroesService, HeroesService>();

            services.AddAuthorization(options =>
            {
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

            return builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(ClubsModuleExtensions).Assembly, "ClubsModule.assets.src"),
                RequestPath = "/manager/clubsmodule"
            });
        }
    }
}