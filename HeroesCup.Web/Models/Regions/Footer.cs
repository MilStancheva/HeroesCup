using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models.Regions
{
    public class Footer
    {
        [Field]
        public HtmlField UpperContentLeft { get; set; }

        [Field]
        public HtmlField UpperContentRight { get; set; }

        [Field]
        public HtmlField MiddleContent { get; set; }
    }
}