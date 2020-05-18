using System;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class ClubListModel
    {
        public IEnumerable<ClubListItem> Clubs { get; set; }
    }

    public class ClubListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OrganizationType { get; set; }

        public string OrganizationName { get; set; }

        public int HeroesCount { get; set; }
    }
}
