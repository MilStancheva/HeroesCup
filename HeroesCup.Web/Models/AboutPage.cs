using HeroesCup.Web.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace HeroesCup.Web.Models
{
    [PageType(Title = "About page", UsePrimaryImage = false, UseExcerpt = false)]
    [PageTypeRoute(Title = "About", Route = "/about")]
    public class AboutPage: Page<AboutPage>
    {
        [Region(Title="Indroduction")]
        public AboutRegion Introduction { get; set; }

        [Region(Title = "First Content")]
        public AboutRegion FirstContent { get; set; }

        [Region(Title = "Second Content")]
        public AboutRegion SecondContent { get; set; }
    }
}
