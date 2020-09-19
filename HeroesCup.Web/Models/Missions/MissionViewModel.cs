using HeroesCup.Data.Models;
using System;

namespace HeroesCup.Web.Models
{
    public class MissionViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public Club Club { get; set; }

        public string ClubName { get; set; }

        public string PostClubName { get; set; }

        public string ClubLocation { get; set; }

        public MissionContent Content { get; set; }

        public string ImageFilename { get; set; }

        public string ImageId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StoryViewModel Story { get; set; }

        public bool IsExpired { get; set; }

        public bool IsSeveralDays { get; set; }
    }
}