using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Models
{
    [PageType(Title = "Missions archive", UseBlocks = false, IsArchive = true)]
    [PageTypeRoute(Title = "Default", Route = "/missions")]
    public class MissionsArchive : Page<MissionsArchive>
    {
        /// <summary>
        /// Gets/sets the post archive.
        /// </summary>
        public PostArchive<LinkMissionPost> LinkMissionArchive { get; set; }

        /// <summary>
        /// Gets/sets the post archive.
        /// </summary>
        public PostArchive<BlogMissionPost> BlogMissionArchive { get; set; }
    }
}
