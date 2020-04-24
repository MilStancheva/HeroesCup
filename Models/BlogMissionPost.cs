using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

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
        public Hero Hero { get; set; }

        [Region(Title = "Детайли")]
        public BlogMissionRegion Details { get; set; }

        [Region(Title = "Участници")]
        public BlogMissionParticipantsRegion Participants { get; set; }

        [Region(Title = "Разказ")]
        public BlogMissionStoryRegion Story { get; set; }


    }
}
