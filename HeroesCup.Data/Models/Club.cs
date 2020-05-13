using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Club
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationType { get; set; }

        public string Location { get; set; }

        public int Points { get; set; }

        public ICollection<Hero> Heroes { get; set; }

        public ICollection<Mission> Missions { get; set; }
    }
}
