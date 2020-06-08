using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Web.Models
{
    [PageType(Title = "About page")]
    [PageTypeRoute(Title = "About", Route = "/about")]
    public class AboutPage: Page<AboutPage>
    {

    }
}
