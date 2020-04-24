using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace HeroesCup.Models
{
    [PostType(Title = "Link Mission Post")]
    [PostTypeRoute(Title = "Default", Route = "/link-mission")]
    public class LinkMissionPost : Post<LinkMissionPost>
    {
        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region]
        public Hero Hero { get; set; }

        /// <summary>
        /// Gets/sets the post details.
        /// </summary>
        [Region(Title="Детайли")]
        public LinkMissionRegion Details { get; set; }
    }
}
