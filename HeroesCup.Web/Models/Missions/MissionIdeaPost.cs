using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Mission idea post")]
    [PostTypeRoute(Title = "Default", Route = "/mission-idea")]
    public class MissionIdeaPost : Post<MissionIdeaPost>
    {
        public MissionIdeaViewModel MissionIdea { get; set; }
    }
}
