using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        public byte[] Bytes { get; set; }

        public string Filename { get; set; }

        public string ContentType { get; set; }

        public ICollection<ClubImage> ClubImages { get; set; }

        public ICollection<MissionImage> MissionImages { get; set; }

        public ICollection<StoryImage> StoryImages { get; set; }

    }
}