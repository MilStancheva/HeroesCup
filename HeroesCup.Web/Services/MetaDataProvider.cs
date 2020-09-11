using HeroesCup.Web.Common;
using HeroesCup.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HeroesCup.Web.Services
{
    public class MetaDataProvider : IMetaDataProvider
    {
        private readonly IConfiguration configuration;
        private readonly IWebUtils webUtils;

        public MetaDataProvider(IConfiguration configuration, IWebUtils webUtils)
        {
            this.configuration = configuration;
            this.webUtils = webUtils;
        }

        public SocialNetworksMetaData getMetaData(HttpContext httpContext, string slug, string title, string url = null, string imageUrl = null, string videoUrl = null, string videoType = null)
        {
            var currentUrlBase = webUtils.GetUrlBase(httpContext);
            var image = imageUrl != null ? imageUrl : $"{currentUrlBase}/{this.configuration["FacebookDefaultImageUrl"]}";
            var validUrl = url != null ? url : $"{currentUrlBase}/{slug}";
            return new SocialNetworksMetaData()
            {
                FacebookAppId = this.configuration["FacebookAppId"],
                Url = validUrl,
                Description = title,
                Title = title,
                Type = this.configuration["FacebookArticleType"],
                Image = image,
                ImageWidth = this.configuration["FacebookDefaultImageWidth"],
                ImageHeight = this.configuration["FacebookDefaultImageHeight"],
                VideoUrl = videoUrl != null ? videoUrl : null,
                VideoType = videoType != null ? videoType : null,
                UrlBase = currentUrlBase,
                TwitterUrl = string.Format(this.configuration["TwitterShareUrl"], validUrl, title)       
            };
        }
    }
}
