using CSMarketApp.Domain.Core.Models.ItemsModels.Class;

namespace CSMarketApp.Domain.Core.Models.ItemsModels.SubClass
{
    public class ItemsSubClass
    {
        public int IdItemSubClass { get; set; }
        public string? ItemSubClassName { get; set; }
        public List<ItemsClass> ItemsClasses { get; set; }
    }
}
