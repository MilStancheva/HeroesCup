using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace HeroesCup.Web.Common
{
    public static class ImageConverter
    {
        public static Image ConvertByteArrayToImage(this byte[] byteArray)
        {
            var image = Image.LoadPixelData<Rgba32>(byteArray, 10, 10);            
            return image;
        }
    }
}
