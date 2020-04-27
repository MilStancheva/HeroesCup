using Piranha.Extend;

namespace HeroesCup.Models
{
    [BlockGroupType(Name = "Участници", Category = "Content")]
    [BlockItemType(Type = typeof(NameBlock))]
    public class StringFieldBlockGroup : BlockGroup
    {
    }
}
