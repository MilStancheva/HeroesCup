using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models.Regions
{
    public class BlogMissionStoryRegion
    {
        [Field]
        public StringField StoryTitle { get; set; }

        [Field]
        public ImageField StoryImage { get; set; }


        [Field]
        public HtmlField StoryContent { get; set; }
    }
}
