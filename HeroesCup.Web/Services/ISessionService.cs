using Microsoft.AspNetCore.Http;

namespace HeroesCup.Web.Services
{
    public interface ISessionService
    {
        int GetCurrentPageCount(HttpContext httpContext, bool loadRequest, string pageCountKey);
    }
}
