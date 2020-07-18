﻿using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Web.Models.Events
{
    [PostType(Title = "Events post", UsePrimaryImage = false, UseExcerpt = false)]
    [PostTypeRoute(Title = "Default", Route = "/event")]
    public class EventPost : Post<EventPost>
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

        public EventPost()
        {
            OtherEvents = new List<EventPost>();
        }
    }
}