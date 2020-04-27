using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models
{
    [BlockType(Name = "Name", Category = "Content", Icon = "fas fa-use")]
    public class NameBlock : Block
    {
        [Field]
        public StringField Value { get; set; }
    }
}
