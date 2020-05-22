using ClubsModule.Attributes;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class StoryEditModel
    {
        public Story Story { get; set; }

        public IEnumerable<Mission> Missions { get; set; }

        [MaxSizeFile(2 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is {0} bytes.")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Allowed file exensions: {0}")]
        public IFormFile UploadedImage { get; set; }

        public string ImageSrc { get; set; }
    }
}