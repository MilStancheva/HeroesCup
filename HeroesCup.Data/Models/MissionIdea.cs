using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class MissionIdea
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public ICollection<MissionIdeaImage> MissionIdeaImages { get; set; }

        public string Content { get; set; }

        public string TimeheroesUrl { get; set; }

        public bool IsPublished { get; set; }
    }
}