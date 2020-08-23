using Microsoft.AspNetCore.Http;
using System.IO;

namespace HeroesCup.Web.Common
{
    public static class ImageConverter
    {
        public static IFormFile ConvertByteArrayToImage(this byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                IFormFile file = new FormFile(stream, 0, byteArray.Length, "name", "image.png");
                return file;
            }
        }
    }
}
