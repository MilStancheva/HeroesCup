using System;

namespace HeroesCup.Data.Models
{
    public class MissionIdeaImage
    {
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public Guid? MissionIdeaId { get; set; }
        public MissionIdea MissionIdea { get; set; }
    }
}
