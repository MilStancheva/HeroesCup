using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesCup.Data.Models
{
    public class HeroMission
    {
        public Guid HeroId { get; set; }
        public Hero Hero { get; set; }

        public Guid MissionId { get; set; }
        public Mission Mission { get; set; }
    }
}
