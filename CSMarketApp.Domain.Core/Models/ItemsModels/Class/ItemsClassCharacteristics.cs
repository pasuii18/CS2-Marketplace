namespace CSMarketApp.Domain.Core.Models.ItemsModels.Class
{
    public class ItemsClassCharacteristics
    {
        public int IdItemClass { get; set; }
        public int IdClassCharacteristic { get; set; }
        public string? ClassCharacteristicValue { get; set; }
        public ItemsClass ItemClass { get; set; }
        public ClassCharacteristics ClassCharacteristic { get; set; }
    }
}
