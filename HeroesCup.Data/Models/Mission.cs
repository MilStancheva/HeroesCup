using System;
using System.Collections.Generic;

namespace HeroesCup.Data.Models
{
    public class Mission
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public MissionType Type { get; set; }

        public string Location { get; set; }

        public long StartDate { get; set; }

        public long EndDate { get; set; }

        public int SchoolYear { get; set; }

        public int Stars { get; set; }

        public Guid ClubId { get; set; }
        public Club Club { get; set; }

        public ICollection<HeroMission> HeroMissions { get; set; }

        public string Content { get; set; }

        public string TimeheroesUrl { get; set; }

        public Guid OwnerId { get; set; }
    }

    public enum MissionType
    {
        TimeheroesLink = 0,
        HeroesCup = 1
    }
}