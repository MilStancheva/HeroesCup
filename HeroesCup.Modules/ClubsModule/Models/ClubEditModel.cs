using HeroesCup.Data.Models;
using Piranha.Extend;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class ClubEditModel
    {
        public Club Club { get; set; }

        public Hero Coordinator { get; set; }

        public Guid CoordinatorId { get; set; }

        [Region]
        public MediaField Logo { get; set; }

        public IEnumerable<Hero> Heroes { get; set;}

        public IEnumerable<Guid> HeroesIds { get; set; }

        public IEnumerable<Mission> Missions { get; set; }

        public IEnumerable<Guid> MissionsIds { get; set; }
    }
}
