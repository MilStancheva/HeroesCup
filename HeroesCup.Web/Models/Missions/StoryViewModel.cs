using System;
using System.Collections.Generic;

namespace HeroesCup.Web.Models
{
    public class StoryViewModel
    {
        public Guid Id { get; set; }

        public MissionViewModel Mission { get; set; }

        public string Content { get; set; }

        public string HeroImageFilename { get; set; }

        public string HeroImageId { get; set; }

        public IEnumerable<string> ImageIds { get; set; }

        public string ClubName { get; set; }
    }
}
