using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeroesCup.Data.Models
{
    public class Mission
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public MissionType Type { get; set; }

        public string Location { get; set; }

        public long StartDate { get; set; }

        public long EndDate { get; set; }

        public string SchoolYear { get; set; }

        public int Stars { get; set; }

        public Guid ClubId { get; set; }
        public Club Club { get; set; }

        public ICollection<HeroMission> HeroMissions { get; set; }

        public ICollection<MissionImage> MissionImages { get; set; }

        public string Content { get; set; }

        public string TimeheroesUrl { get; set; }

        public Guid OwnerId { get; set; }

        public bool isPublished { get; set; }
    }

    public enum MissionType
    {
        [Display(Name = "Timeheroes Mission")]
        TimeheroesMission = 0,

        [Display(Name = "HeroesCup Mission")]
        HeroesCupMission = 1
    }
}