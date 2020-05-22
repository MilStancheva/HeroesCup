using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesCup.Data.Models
{
    public class ClubImage
    {
        public Guid? ImageId { get; set; }
        public Image Image { get; set; }

        public Guid? ClubId { get; set; }
        public Club Club { get; set; }
    }
}