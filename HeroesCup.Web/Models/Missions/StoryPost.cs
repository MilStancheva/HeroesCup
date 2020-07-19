using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Story post")]
    [PostTypeRoute(Title = "Default", Route = "/story")]
    public class StoryPost : Post<StoryPost>
    {
        public StoryViewModel Story { get; set; }
    }
}
