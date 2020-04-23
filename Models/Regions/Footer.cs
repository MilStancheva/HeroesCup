using Piranha.Extend;
using Piranha.Extend.Fields;
using System.Collections.Generic;

namespace HeroesCup.Models.Regions
{
    public class Footer
    {
        [Field(Title = "Footer logo 1")]
        public ImageField LogoImages1 { get; set; }

        [Field(Title = "Footer logo 2")]
        public ImageField LogoImages2 { get; set; }

        [Field(Title = "Footer text")]
        public HtmlField Text { get; set; }
    }
}
