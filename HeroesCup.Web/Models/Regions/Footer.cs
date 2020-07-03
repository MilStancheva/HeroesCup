using Piranha.Extend;
using Piranha.Extend.Fields;
using System.Collections.Generic;

namespace HeroesCup.Models.Regions
{
    public class Footer
    {
        [Field]
        public HtmlField MainPartnerInfoBox { get; set; }

        [Field]
        public ImageField PartnerLogo1 { get; set; }

        [Field]
        public StringField PartnerUrl1 { get; set; }

        [Field]
        public ImageField PartnerLogo2 { get; set; }

        [Field]
        public StringField PartnerUrl2 { get; set; }

        [Field]
        public ImageField PartnerLogo3 { get; set; }

        [Field]
        public StringField PartnerUrl3 { get; set; }

        [Field]
        public ImageField PartnerLogo4 { get; set; }

        [Field]
        public StringField PartnerUrl4 { get; set; }

        [Field]
        public ImageField PartnerLogo5 { get; set; }

        [Field]
        public StringField PartnerUrl5 { get; set; }

        [Field]
        public ImageField PartnerLogo6 { get; set; }

        [Field]
        public StringField PartnerUrl6 { get; set; }
    }
}