using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Data;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;
using System.Globalization;

namespace HeroesCup.Web.Models.Resources
{
    [PostType(Title = "Resource post", UsePrimaryImage = false, UseExcerpt = false)]
    [PostTypeRoute(Title = "Default", Route = "/resource")]
    public class ResourcePost : Post<ResourcePost>, IHeroesCupPost
    {
        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region]
        public HeroRegion Hero { get; set; }

        [Region(Title = "Subtitle")]
        public StringField Subtitle { get; set; }

        [Region(Title = "Type of resource")]
        public SelectField<ResourcePostType> Type { get; set; }

        public IEnumerable<ResourcePost> OtherResources { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }

        public string VideoThumbnail { get; set; }

        public string VideoUrl { get; set; }

        public ResourcePost()
        {
            OtherResources = new List<ResourcePost>();
        }
    }

    public enum ResourcePostType
    {
        PDF,
        VIDEO,
        ARTICLE
    }
}