using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Mission
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Location { get; set; }

        public long StartDate { get; set; }

        public long EndDate { get; set; }

        public int DurationInHours { get; set; }

        public string SchoolYear { get; set; }

        public int Stars { get; set; }

        public Guid ClubId { get; set; }
        public Club Club { get; set; }

        public ICollection<HeroMission> HeroMissions { get; set; }

        public ICollection<MissionImage> MissionImages { get; set; }

        public MissionContent Content { get; set; }

        public Guid OwnerId { get; set; }

        public bool IsPublished { get; set; }

        public bool IsPinned { get; set; }

        public Story Story { get; set; }

        public long CreatedOn { get; set; }

        public long UpdatedOn { get; set; }
    }
}