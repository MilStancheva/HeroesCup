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
                        
            return services;
        }
        public static void MapClubsModule(this IEndpointRouteBuilder builder)
        {
            builder.MapRazorPages();
        }
        public static IApplicationBuilder UseClubsModule(this IApplicationBuilder builder)
        {
            Menu.Items.Insert(2, new MenuItem
            {
                InternalId = "ClubsModule",
                Name = "Clubs",
                Css = "fas fa-fish"
            });
            Menu.Items["ClubsModule"].Items.Add(new MenuItem
            {
                InternalId = "Clubs",
                Name = "List",
                Route = "~/manager/clubs",
                Css = "fas fa-brain",
                // Policy = "MyCustomPolicy"
            });
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