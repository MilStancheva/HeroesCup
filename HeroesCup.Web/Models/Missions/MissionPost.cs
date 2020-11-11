using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Globalization;

namespace HeroesCup.Web.Models.Missions
{
    [PostType(Title = "Mission post")]
    [ContentTypeRoute(Title = "Default", Route = "/mission")]
    public class MissionPost : Post<MissionPost>, IHeroesCupPost, ISocialNetworkPost
    {
        public MissionViewModel Mission { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }

        public SocialNetworksMetaData SocialNetworksMetaData { get; set; }

        public string StartDateAsLocalString { get; set; }

        public string EndDateAsLocalString { get; set; }
    }
}