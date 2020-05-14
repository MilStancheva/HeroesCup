using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace HeroesCup.Web.Models.Events
{
    [PageType(Title = "Events archive", UseBlocks = false, IsArchive = true)]
    [PageTypeRoute(Title = "Default", Route = "/events")]
    public class EventsArchive : Page<EventsArchive>
    {
        /// <summary>
        /// Gets/sets the archive hero.
        /// </summary>
        [Region(Display = RegionDisplayMode.Setting)]
        public HeroRegion Hero { get; set; }

        /// <summary>
        /// Gets/sets the resource post archive.
        /// </summary>
        public PostArchive<EventPost> Archive { get; set; }
    }
}