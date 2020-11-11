using HeroesCup.Web.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace HeroesCup.Web.Models
{
    [PageType(Title = "About page", UsePrimaryImage = false, UseExcerpt = false)]
    [ContentTypeRoute(Title = "About", Route = "/about")]
    public class AboutPage: Page<AboutPage>, ISocialNetworkPost
    {
        [Region(Title="Indroduction")]
        public AboutRegion Introduction { get; set; }

        [Region(Title = "First Content")]
        public AboutRegion FirstContent { get; set; }

        [Region(Title = "Second Content")]
        public AboutRegion SecondContent { get; set; }

        public SocialNetworksMetaData SocialNetworksMetaData { get; set; }
    }
}
