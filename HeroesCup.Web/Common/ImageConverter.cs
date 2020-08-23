using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace HeroesCup.Web.Common
{
    public static class ImageConverter
    {
        public static IFormFile ToFormFile(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                IFormFile file = new FormFile(stream, 0, byteArray.Length, "name", "fileName");
                return file;
            }
        }
    }
}
