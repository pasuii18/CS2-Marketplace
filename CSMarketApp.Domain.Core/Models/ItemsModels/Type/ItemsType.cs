using CSMarketApp.Domain.Core.Models.ItemsModels.Class;

namespace CSMarketApp.Domain.Core.Models.ItemsModels.Type
{
    public class ItemsType
    {
        public int IdItemType { get; set; }
        public int IdItemClass { get; set; }
        public string? ItemTypeName { get; set; }
        public List<Items> Items { get; set; }
        public ItemsClass ItemsClass { get; set; }
        public List<ItemsTypeCharacteristics> ItemsTypeCharacteristics { get; set; }
    }
}
