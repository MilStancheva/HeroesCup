using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Story
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public Guid MissionId { get; set; }
        public Mission Mission { get; set; }

        public ICollection<StoryImage> StoryImages { get; set; }

    }
}
