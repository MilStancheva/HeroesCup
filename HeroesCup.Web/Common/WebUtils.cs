using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Piranha;
using System.Threading.Tasks;

namespace HeroesCup.Web.Common
{
    public class WebUtils : IWebUtils
    {
        private readonly IConfiguration configuration;

        public WebUtils(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetUrlBase(HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        }

        public async Task<System.Globalization.CultureInfo> GetCulture(IApi api)
        {
            var site = await api.Sites.GetDefaultAsync();
            var siteCulture = site.Culture != null ? site.Culture : this.configuration["DefaultSiteCulture"];
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(siteCulture);
            return culture;
        }
    }
}
