using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Web.Models.Blocks
{
    [BlockType(Name = "Streaming Video", Category = "Media",
    Icon = "fas fa-video", Component = "streaming-video-block")]
    public class StreamingVideoBlock : Block
    {
        /// <summary>
        /// Gets/sets the source of the streaming video.
        /// </summary>
        public StringField Source { get; set; }
    }
}
