using Microsoft.AspNetCore.Http;
using Piranha;
using System.Threading.Tasks;

namespace HeroesCup.Web.Common
{
    public interface IWebUtils
    {
        string GetUrlBase(HttpContext httpContext);

        Task<System.Globalization.CultureInfo> GetCulture(IApi api);
    }
}