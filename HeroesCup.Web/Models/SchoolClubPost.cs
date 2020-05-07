using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Models
{
    [PostType(Title = "School Club")]
    [PostTypeRoute(Title = "Default", Route = "/club")]
    public class SchoolClubPost : Post<SchoolClubPost>
    {
        [Region(ListTitle = "Region")]
        public SchoolClubRegion SchoolClubRegion { get; set; }

        [Region]
        public IList<StringField> Participants { get; set; }

        [Region(ListTitle = "Missions")]
        public IList<BlogMissionPost> Missions { get; set; }

        public int Points { get; set; }

        public SchoolClubPost()
        {
            Missions = new List<BlogMissionPost>();
        }
    }
}
