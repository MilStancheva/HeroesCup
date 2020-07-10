using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeroesCup.Data.Models
{
    public class Story
    {
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid MissionId { get; set; }
        public Mission Mission { get; set; }

        public ICollection<StoryImage> StoryImages { get; set; }

        public bool IsPublished { get; set; }

    }
}