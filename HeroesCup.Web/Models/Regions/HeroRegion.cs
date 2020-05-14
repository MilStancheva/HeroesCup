using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models.Regions
{
    public class HeroRegion
    {
        /// <summary>
        /// Gets/sets the optional primary image.
        /// </summary>
        [Field(Title = "Главно изображение")]
        public ImageField PrimaryImage { get; set; }

        /// <summary>
        /// Gets/sets the optional ingress title.
        /// </summary>
        [Field(Title = "Заглавие")]
        public TextField IngressTitle { get; set; }

        /// <summary>
        /// Gets/sets the optional ingress.
        /// </summary>
        [Field(Title = "Текст")]
        public TextField Ingress { get; set; }
    }
}