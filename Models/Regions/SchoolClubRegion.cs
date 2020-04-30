using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;

namespace HeroesCup.Models.Regions
{
    public class SchoolClubRegion
    {
        /// <summary>
        /// Gets/sets the optional logo image.
        /// </summary>
        [Field(Title = "School Club Logo Image")]
        public ImageField PrimaryImage { get; set; }

        [Field(Options = FieldOption.HalfWidth, Title = "Coordinator")]
        public StringField Coordinator { get; set; }

        /// <summary>
        /// Gets/sets the main team title.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth, Title = "School Club's Name")]
        public StringField TeamTitle { get; set; }

        [Field(Options = FieldOption.HalfWidth, Title = "Location")]
        public StringField Location { get; set; }
    }
}
