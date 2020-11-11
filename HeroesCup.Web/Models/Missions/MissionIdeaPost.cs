using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Globalization;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Mission idea post")]
    [ContentTypeRoute(Title = "Default", Route = "/mission-idea")]
    public class MissionIdeaPost : Post<MissionIdeaPost>, IHeroesCupPost, ISocialNetworkPost
    {
        public MissionIdeaViewModel MissionIdea { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }

        public SocialNetworksMetaData SocialNetworksMetaData { get; set; }

        public string StartDateAsLocalString { get; set; }

        public string EndDateAsLocalString { get; set; }
    }
}
