using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Globalization;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Mission post")]
    [PostTypeRoute(Title = "Default", Route = "/mission")]
    public class MissionPost : Post<MissionPost>, IHeroesCupPost
    {
        public MissionViewModel Mission { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }
    }
}