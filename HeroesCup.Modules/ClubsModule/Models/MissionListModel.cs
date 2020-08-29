using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class MissionListModel
    {
        public IEnumerable<MissionListItem> Missions { get; set; }
    }

    public class MissionListItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid ClubId { get; set; }

        public string ClubName { get; set; }

        public int HeroesCount { get; set; }

        public bool IsPublished { get; set; }

        public bool IsPinned { get; set; }

        public string LastUpdateOn { get; set; }
    }
}