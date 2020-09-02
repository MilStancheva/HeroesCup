using Microsoft.Extensions.Configuration;

namespace HeroesCup.Web.Services
{
    public class YouTubeVideoThumbnailParser : IVideoThumbnailParser
    {
        private readonly IConfiguration configuration;

        public YouTubeVideoThumbnailParser(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ParseDefaultThubnailUrl(string embeddedVideoUrl)
        {
            if (string.IsNullOrEmpty(embeddedVideoUrl) || string.IsNullOrWhiteSpace(embeddedVideoUrl))
            {
                return null;
            }

            if (embeddedVideoUrl.IndexOf(this.configuration["YouTubeUrl"]) < 0)
            {
                return null;
            }

            var embedUrlPart = this.configuration["YouTubeEmbedUrlPart"];
            var index = embeddedVideoUrl.IndexOf(embedUrlPart);
            if (index != -1)
            {
                var videoCode = embeddedVideoUrl.Substring(index + embedUrlPart.Length);
                return $"{this.configuration["YouTubeThumbnailUrl"]}/{videoCode}/{this.configuration["YouTubeThumbnailDefaultImageName"]}";
            }

            return null;
        }
    }
}
