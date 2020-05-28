using Piranha.AttributeBuilder;
using Piranha.Models;

namespace HeroesCup.Web.Models
{
    [PageType(Title = "Landing page")]
    [PageTypeRoute(Title = "Landing", Route = "/landing")]
    public class LandingPage : Page<LandingPage>
    {

    }
}