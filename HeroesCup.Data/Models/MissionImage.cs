using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesCup.Data.Models
{
    public class MissionImage
    {
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public Guid? MissionId { get; set; }
        public Mission Mission { get; set; }
    }
}
