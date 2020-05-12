using HeroesCup.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class HeroEditModel
    {
        public Hero Hero { get; set; }

        public IEnumerable<Mission> Missions { get; set; }
    }
}
