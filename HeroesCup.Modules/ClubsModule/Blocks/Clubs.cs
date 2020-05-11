using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Modules.ClubsModule.Blocks
{
    /// <summary>
    /// Single column text block.
    /// </summary>
    [BlockType(Name = "Clubs", Category = "Content", Icon = "fas fa-heading", Component = "clubs")]
    public class Clubs : Block
    {
        /// <summary>
        /// Gets/sets the text body.
        /// </summary>
        public TextField Body { get; set; }

        public override string GetTitle()
        {
            if (Body.Value != null)
            {
                return Body.Value;
            }
            return "Hello clubs!";
        }
    }
}
