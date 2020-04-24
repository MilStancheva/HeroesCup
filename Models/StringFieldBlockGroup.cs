using Piranha.Extend;

namespace HeroesCup.Models
{
    [BlockGroupType(Name = "Участници", Category = "Content")]
    [BlockItemType(Type = typeof(StringFieldBlock))]
    public class StringFieldBlockGroup : BlockGroup
    {
    }
}
