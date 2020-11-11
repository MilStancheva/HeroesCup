using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Globalization;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Story post")]
    [ContentTypeRoute(Title = "Default", Route = "/story")]
    public class StoryPost : Post<StoryPost>, IHeroesCupPost, ISocialNetworkPost
    {
        public StoryViewModel Story { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }

        public SocialNetworksMetaData SocialNetworksMetaData { get; set; }

        public string StartDateAsLocalString { get; set; }

        public string EndDateAsLocalString { get; set; }
    }
}
