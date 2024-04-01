using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;

namespace CSMarketApp.Domain.Core.Models.ItemsModels.Class
{
    public class ItemsClass
    {
        public int IdItemClass { get; set; }
        public int IdItemSubClass { get; set; }
        public string? ItemClassName { get; set; }
        public List<ItemsType> ItemsTypes { get; set; }
        public ItemsSubClass ItemsSubClass { get; set; }
        public List<ItemsClassCharacteristics> ItemsClassCharacteristics { get; set; }
    }
}
