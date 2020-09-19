using HeroesCup.Data.Models;
using System;

namespace HeroesCup.Web.Models
{
    public class MissionIdeaViewModel
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public MissionIdea MissionIdea { get; set; }

        public string ImageFilename { get; set; }

        public string ImageId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsExpired { get; set; }

        public bool IsSeveralDays { get; set; }

        public string Organization { get; set; }
    }
}
