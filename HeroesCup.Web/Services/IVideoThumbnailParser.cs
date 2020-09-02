namespace HeroesCup.Web.Services
{
    public interface IVideoThumbnailParser
    {
        string ParseDefaultThubnailUrl(string embeddedVideoUrl);
    }
}
