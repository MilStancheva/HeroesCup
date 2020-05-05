using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Models
{
    [PostType(Title = "Блог мисия")]
    [PostTypeRoute(Title = "Default", Route = "/blog-mission")]
    public class BlogMissionPost : Post<BlogMissionPost>
    {
        /// <summary>
        /// Gets/sets the post hero.
        /// </summary>
        [Region(Title="Главно изображение")]
        public HeroRegion Hero { get; set; }

        [Region(Title = "Детайли")]
        public BlogMissionRegion Details { get; set; }

        [Region(Title = "Участници")]
        public IList<StringField> Participants { get; set; }

        [Region(Title = "Разказ")]
        public BlogMissionStoryRegion Story { get; set; }
    }
}
