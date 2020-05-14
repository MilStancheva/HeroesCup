using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;

namespace HeroesCup.Models
{
    [SiteType(Title = "Heores Cup site")]
    public class HeroesCupSite : SiteContent<HeroesCupSite>
    {
        [Region(Title = "Footer")]
        public Footer FooterContents { get; set; }
    }
}