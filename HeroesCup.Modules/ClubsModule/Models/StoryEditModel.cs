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

        [MaxSizeFile(2 * 1024 * 1024, ErrorMessage = "MaxSizeErrorMessage")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "AllowedFileExtensionsErrorMessage")]
        public ICollection<IFormFile> UploadedImages { get; set; }

        public ICollection<string> ImageSources { get; set; }
    }
}