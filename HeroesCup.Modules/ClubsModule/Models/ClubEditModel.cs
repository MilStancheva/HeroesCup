using HeroesCup.Data.Models;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class ClubEditModel
    {
        public Club Club { get; set; }

        public IEnumerable<Hero> Heroes { get; set;}

        public IEnumerable<Mission> Missions { get; set; }
    }
}
