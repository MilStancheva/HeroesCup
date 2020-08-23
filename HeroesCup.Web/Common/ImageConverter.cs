using System;

namespace HeroesCup.Web.Common
{
    public static class ImageConverter
    {
        public static byte[] GetBase64Blob(this string base64string)
        {
            byte[] blob = Convert.FromBase64String(base64string);
            return blob;
        }
    }
}
