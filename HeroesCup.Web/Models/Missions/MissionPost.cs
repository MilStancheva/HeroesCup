using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Mission post")]
    [PostTypeRoute(Title = "Default", Route = "/mission")]
    public class MissionPost : Post<MissionPost>
    {
        public MissionViewModel Mission { get; set; }
    }
}