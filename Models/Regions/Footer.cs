using Piranha.Extend;
using Piranha.Extend.Fields;
using System.Collections.Generic;

namespace HeroesCup.Models.Regions
{
    public class Footer
    {
        [Field]
        public HtmlField UpperContent { get; set; }

        [Field]
        public HtmlField MiddleContent { get; set; }
    }
}
