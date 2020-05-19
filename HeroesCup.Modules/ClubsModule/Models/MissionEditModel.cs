using ClubsModule.Attributes;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClubsModule.Models
{
    public class MissionEditModel
    {
        public Mission Mission { get; set; }

        [MaxSizeFile(2 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is {0} bytes.")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "Allowed file exensions: {0}")]
        public IFormFile Image { get; set; }

        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadedStartDate { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime UploadedEndDate { get; set; } = DateTime.UtcNow;

        public string ImageSrc { get; set; }

        public IEnumerable<Hero> Heroes { get; set; }

        public IEnumerable<Guid> HeroesIds { get; set; }

        public IEnumerable<Club> Clubs { get; set; }

        public IEnumerable<Guid> ClubsIds { get; set; }
    }
}
