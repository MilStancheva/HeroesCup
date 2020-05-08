using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Data;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace HeroesCup.Web.Models.Resources
{
    [PostType(Title = "Resource post")]
    [PostTypeRoute(Title = "Default", Route = "/resource")]
    public class ResourcePost : Post<ResourcePost>
    {
        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region]
        public HeroRegion Hero { get; set; }

        [Region(Title="Type of resource")]
        public SelectField<ResourcePostType> Type { get; set; }
    }

    public enum ResourcePostType
    {
        PDF,
        VIDEO,
        ARTICLE
    }
}
