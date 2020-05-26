using System;
using System.Collections.Generic;

namespace HeroesCup.Web.Models
{
    public class ClubListViewModel
    {
        public IEnumerable<ClubListItem> Clubs { get; set; }
    }

    public class ClubListItem
    {
        public Guid Id { get; set; }

        public string ClubInitials { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int Points { get; set; }

        public int HeroesCount { get; set; }
    }
}
