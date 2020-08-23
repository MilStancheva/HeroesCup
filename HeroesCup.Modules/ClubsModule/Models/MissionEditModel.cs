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

        [MaxSizeFile(2 * 1024 * 1024, ErrorMessage = "MaxSizeErrorMessage")]
        [AllowedExtensions(new string[] { ".jpg", ".jpеg", ".png" }, ErrorMessage = "AllowedFileExtensionsErrorMessage")]
        public IFormFile Image { get; set; }

        [Required]
        public string UploadedStartDate { get; set; }

        [Required]
        public string UploadedEndDate { get; set; }

        public string ImageSrc { get; set; }

        public byte[] ImageBytes { get; set; }

        public IEnumerable<Club> Clubs { get; set; }

        public Guid ClubId { get; set; }

        public TimeSpan Duration { get; set; }
    }
}