using System;

namespace HeroesCup.Data.Models
{
    public class HeroMission
    {
        public Guid Id { get; set; }

        public Guid HeroId { get; set; }
        public Hero Hero { get; set; }

        public Guid MissionId { get; set; }
        public Mission Mission { get; set; }
    }
}
