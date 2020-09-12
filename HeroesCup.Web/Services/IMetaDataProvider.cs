using HeroesCup.Web.Models;
using Microsoft.AspNetCore.Http;

namespace HeroesCup.Web.Services
{
    public interface IMetaDataProvider
    {
        SocialNetworksMetaData getMetaData(HttpContext httpContext,
            string slug,
            string title,
            string url = null,
            string imageUrl = null,
            string videoUrl = null,
            string videoType = null);
    }
}
