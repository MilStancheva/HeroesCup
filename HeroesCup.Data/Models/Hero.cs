using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Hero 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ClubId { get; set; }
        public Club Club { get; set; }

        public bool IsCoordinator { get; set; }

        public ICollection<HeroMission> HeroMissions { get; set; }
    }
}
