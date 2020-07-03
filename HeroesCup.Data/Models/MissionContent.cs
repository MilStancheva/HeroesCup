using System;

namespace HeroesCup.Data.Models
{
    public class MissionContent
    {
        public Guid Id { get; set; }

        public string What { get; set; }

        public string When { get; set; }

        public string Where { get; set; }

        public string Equipment { get; set; }
        
        public string Why { get; set; }

        public Guid MissionId { get; set; }
        public Mission Mission { get; set; }
    }
}
