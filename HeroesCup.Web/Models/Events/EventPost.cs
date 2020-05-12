using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Web.Models.Events
{
    [PostType(Title = "Events post")]
    [PostTypeRoute(Title = "Default", Route = "/event")]
    public class EventPost : Post<EventPost>
    {

        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region]
        public HeroRegion Hero { get; set; }

        public IEnumerable<EventPost> OtherEvents { get; set; }

        public EventPost()
        {
            OtherEvents = new List<EventPost>();
        }
    }
}
