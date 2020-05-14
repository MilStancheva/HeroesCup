using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models.Regions
{
    public class BlogMissionStoryRegion
    {
        [Field(Title = "Заглавие")]
        public StringField StoryTitle { get; set; }

        [Field(Title = "Изображение")]
        public ImageField StoryImage { get; set; }


        [Field(Title = "Съдържание")]
        public HtmlField StoryContent { get; set; }
    }
}