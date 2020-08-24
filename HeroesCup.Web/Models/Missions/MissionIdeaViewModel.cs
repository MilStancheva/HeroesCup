using HeroesCup.Data.Models;
using System;

namespace HeroesCup.Web.Models
{
    public class MissionIdeaViewModel
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public MissionIdea MissionIdea { get; set; }

        public string ImageSrc { get; set; }

        public string ImageFilename { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
