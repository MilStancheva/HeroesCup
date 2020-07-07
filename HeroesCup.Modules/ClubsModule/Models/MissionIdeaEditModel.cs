using ClubsModule.Attributes;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;

namespace ClubsModule.Models
{
    public class MissionIdeaEditModel
    {
        public MissionIdea MissionIdea { get; set; }

        [MaxSizeFile(2 * 1024 * 1024, ErrorMessage = "MaxSizeErrorMessage")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "AllowedFileExtensionsErrorMessage")]
        public IFormFile Image { get; set; }

        public string ImageSrc { get; set; }
    }
}