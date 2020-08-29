using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Globalization;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Story post")]
    [PostTypeRoute(Title = "Default", Route = "/story")]
    public class StoryPost : Post<StoryPost>, IHeroesCupPost
    {
        public StoryViewModel Story { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }
    }
}
