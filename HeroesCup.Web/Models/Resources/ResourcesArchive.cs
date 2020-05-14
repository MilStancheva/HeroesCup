using HeroesCup.Models.Regions;
using HeroesCup.Web.Models.Resources;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace HeroesCup.Web.Models.Resources
{
    [PageType(Title = "Resources archive", UseBlocks = false, IsArchive = true)]
    [PageTypeRoute(Title = "Default", Route = "/resources")]
    public class ResourcesArchive : Page<ResourcesArchive>
    {
        /// <summary>
        /// Gets/sets the archive hero.
        /// </summary>
        [Region(Display = RegionDisplayMode.Setting)]
        public HeroRegion Hero { get; set; }

        /// <summary>
        /// Gets/sets the resource post archive.
        /// </summary>
        public PostArchive<ResourcePost> Archive { get; set; }
    }
}