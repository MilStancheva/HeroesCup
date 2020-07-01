using HeroesCup.Data.Models;
using System;

namespace HeroesCup.Web.Models
{
    public class MissionViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Club Club { get; set; }

        public Mission Mission { get; set; }

        public string ImageSrc { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StoryViewModel Story { get; set; }
    }
}