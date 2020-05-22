using System;

namespace HeroesCup.Data.Models
{
    public class StoryImage
    {
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public Guid? StoryId { get; set; }
        public Story Story { get; set; }
    }
}