using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace HeroesCup.Models.Regions
{
    public class LinkMissionRegion
    {
        /// <summary>
        /// Gets/sets the link mission post organizator.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth)]
        public StringField OrganizedBy { get; set; }

        /// <summary>
        /// Gets/sets the link mission post URL of the real mission post.
        /// </summary>
        [Field]
        public StringField URL { get; set; }
    }
}