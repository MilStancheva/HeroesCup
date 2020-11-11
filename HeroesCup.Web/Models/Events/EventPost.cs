using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;
using System.Globalization;

namespace HeroesCup.Web.Models.Events
{
    [PostType(Title = "Events post", UsePrimaryImage = false, UseExcerpt = false)]
    [ContentTypeRoute(Title = "Default", Route = "/event")]
    public class EventPost : Post<EventPost>, IHeroesCupPost, ISocialNetworkPost
    {

        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region]
        public HeroRegion Hero { get; set; }

        /// <summary>
        /// Gets/sets the post author.
        /// </summary>
        [Region(Title = "Author")]
        public StringField Author { get; set; } = "TimeHeroes";

        public IEnumerable<EventPost> OtherEvents { get; set; }

        public string CurrentUrlBase { get; set; }

        public CultureInfo SiteCulture { get; set; }

        public SocialNetworksMetaData SocialNetworksMetaData { get; set; }

        public EventPost()
        {
            OtherEvents = new List<EventPost>();
        }
    }
}