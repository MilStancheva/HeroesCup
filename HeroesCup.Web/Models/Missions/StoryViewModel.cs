using System;
using System.Collections.Generic;

namespace HeroesCup.Web.Models
{
    public class StoryViewModel
    {
        public Guid Id { get; set; }

        public MissionViewModel Mission { get; set; }

        public string Content { get; set; }

        public IEnumerable<string> ImageSources { get; set; }

        public string HeroImageSource { get; set; }

        public string HeroImageFilename { get; set; }
    }
}
