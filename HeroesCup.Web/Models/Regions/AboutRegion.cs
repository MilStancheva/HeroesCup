using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Web.Models.Regions
{
    public class AboutRegion
    {
        [Field(Title = "Заглавие")]
        public TextField Title { get; set; }

        [Field(Title = "Текст")]
        public HtmlField Body { get; set; }
    }
}
