using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Models
{
    [PageType(Title = "School club archive", UseBlocks = false, IsArchive = true)]
    [PageTypeRoute(Title = "Default", Route = "/clubs")]
    public class SchoolClubArchive : Page<SchoolClubArchive>
    {
        /// <summary>
        /// Gets/sets the school club posts archive.
        /// </summary>
        public PostArchive<SchoolClubPost> SchoolClubsArchive { get; set; }
    }
}