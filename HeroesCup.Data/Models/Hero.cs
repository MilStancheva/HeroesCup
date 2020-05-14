using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Hero
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid SchoolClubId { get; set; }
        public SchoolClub SchoolClub { get; set; }

        public bool IsCoordinator { get; set; }

        public ICollection<HeroMission> HeroMissions { get; set; }
    }
}