using Piranha.Extend;
using Piranha.Extend.Fields;

namespace HeroesCup.Models
{
    [BlockType(Name = "Name Item", Category = "Content", IsUnlisted = true)]
    public class StringFieldBlock
    {
        [Field]
        public StringField Value { get; set; }
    }
}
