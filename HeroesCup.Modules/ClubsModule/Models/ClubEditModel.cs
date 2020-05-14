using HeroesCup.Data.Models;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Manager.Localization;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class ClubEditModel
    {
        public Club Club { get; set; }

        [Field]
        public MediaField Logo { get; set; }

        public IEnumerable<Hero> Heroes { get; set;}

        public IEnumerable<Mission> Missions { get; set; }
    }
}
