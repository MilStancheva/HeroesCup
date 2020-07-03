using ClubsModule.Attributes;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;

namespace ClubsModule.Models
{
    public class MissionIdeaEditModel
    {
        public MissionIdea MissionIdea { get; set; }

        [MaxSizeFile(2 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is {0} bytes.")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Allowed file exensions: {0}")]
        public IFormFile Image { get; set; }

        public string ImageSrc { get; set; }
    }
}