using Piranha.Extend;
using Piranha.Extend.Fields;
using System.Collections.Generic;

namespace HeroesCup.Models.Regions
{
    public class BlogMissionParticipantsRegion
    {
        [Region]
        public IList<StringField> Participants { get; set; }

        public BlogMissionParticipantsRegion()
        {
            Participants = new List<StringField>();
        }

    }
}
