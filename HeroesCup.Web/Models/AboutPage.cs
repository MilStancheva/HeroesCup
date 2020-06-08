using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace HeroesCup.Web.Models
{
    [PageType(Title = "About page")]
    [PageTypeRoute(Title = "About", Route = "/about")]
    public class AboutPage: Page<AboutPage>
    {
        [Region(Title="Indroduction")]
        public HtmlField Introduction { get; set; }

        [Region(Title = "First Content")]
        public HtmlField FirstContent { get; set; }

        [Region(Title = "Second Content")]
        public HtmlField SecondContent { get; set; }
    }
}
