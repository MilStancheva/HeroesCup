using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Web.Models.Blocks
{
    [BlockType(Name = "Embedded Video", Category = "Media",
    Icon = "fas fa-video", Component = "embedded-video-block")]
    public class EmbeddedVideoBlock : Block
    {
        /// <summary>
        /// Gets/sets the source of the streaming video.
        /// </summary>
        public StringField Source { get; set; }
    }
}
