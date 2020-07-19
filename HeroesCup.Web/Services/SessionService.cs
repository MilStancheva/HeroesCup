using Microsoft.AspNetCore.Http;

namespace HeroesCup.Web.Services
{
    public class SessionService : ISessionService
    {
        public int GetCurrentPageCount(HttpContext httpContext, bool loadRequest, string pageCountKey)
        {
            int? currentPageCount = null;
            if (loadRequest == true)
            {
                currentPageCount = httpContext.Session.GetInt32(pageCountKey);
                if (currentPageCount == null)
                {
                    currentPageCount = 2;
                    httpContext.Session.SetInt32(pageCountKey, (int)currentPageCount);
                }
                else
                {
                    httpContext.Session.SetInt32(pageCountKey, (int)(currentPageCount += 1));
                }
            }
            else
            {
                currentPageCount = 1;
                httpContext.Session.SetInt32(pageCountKey, (int)currentPageCount);
            }

            return (int)currentPageCount;
        }
    }
}
