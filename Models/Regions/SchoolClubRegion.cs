using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System.Text.RegularExpressions;

namespace HeroesCup.Models.Regions
{
    public class SchoolClubRegion
    {
        /// <summary>
        /// Gets/sets the optional logo image.
        /// </summary>
        [Field(Title = "Лого на клуба")]
        public ImageField PrimaryImage { get; set; }

        [Field(Options = FieldOption.HalfWidth, Title = "Координатор")]
        public StringField Coordinator { get; set; }

        /// <summary>
        /// Gets/sets the type of the school.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth, Title = "Тип на училище")]
        public StringField SchoolType { get; set; }

        /// <summary>
        /// Gets/sets the main team title.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth, Title = "Училище")]
        public StringField SchoolName { get; set; }

        [Field(Options = FieldOption.HalfWidth, Title = "Локация")]
        public StringField Location { get; set; }

        public string Initials
        {
            get
            {
                Regex initials = new Regex(@"(\b[a-zA-Z-а-яА-Я])[a-zA-Z-а-яА-Я]* ?");
                return initials.Replace(this.SchoolName, "$1");
            }
        }
    }
}
