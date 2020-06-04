using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models.Regions
{
    public class Footer
    {
        [Field]
        public HtmlField MainPartnerInfoBox { get; set; }

        [Field]
        public HtmlField OtherPartners { get; set; }
    }
}