using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Piranha;
using Piranha.Manager.Editor;

namespace HeroesCup.Manager.TinyMCE
{
    public static class TinyMCEExtensions
    {
        public static IServiceCollection AddPiranhaTinyMCE(this IServiceCollection services)
        {
            App.Modules.Register<HeroesCup.Manager.TinyMCE.Module>();

            return services;
        }

        public static IApplicationBuilder UsePiranhaTinyMCE(this IApplicationBuilder builder)
        {
            App.Modules.Get<Piranha.Manager.Module>().Styles.Add("~/heroescup/tiny/heroescup.editor.styles.css");
            EditorScripts.MainScriptUrl = "~/heroescup/tiny/tinymce/tinymce.min.js";
            EditorScripts.EditorScriptUrl = "~/heroescup/tiny/heroescup.editor.js";

            return builder.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new EmbeddedFileProvider(typeof(TinyMCEExtensions).Assembly, "HeroesCup.Manager.TinyMCE.assets"),
                RequestPath = "/heroescup/tiny"
            });
        }
    }
}
