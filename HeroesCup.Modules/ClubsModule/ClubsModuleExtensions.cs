using Microsoft.AspNetCore.Builder;
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
                InternalId = "MyCustomItem",
                Name = "My Custom Item",
                Route = "~/manager/mycustomitem",
                Css = "fas fa-brain",
                // Policy = "MyCustomPolicy"
            });
            // Manager resources
            App.Modules.Get<Piranha.Manager.Module>().Scripts.Add("~/manager/ClubsModule/js/header-block.js");
            // App.Modules.Get<Module>().
            // Add the embedded resources
            return builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(ClubsModuleExtensions).Assembly, "ClubsModule.assets.dist"),
                RequestPath = "/manager/ClubsModule"
            });
        }
    }
}