using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubsModule.Models
{
    public class HeroListModel
    {
        public IEnumerable<HeroListItem> Heroes { get; set; }
    }

    public class HeroListItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OrganizationName { get; set; }
    }
}
