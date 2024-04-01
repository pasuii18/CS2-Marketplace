namespace CSMarketApp.Domain.Core.Models.ItemsModels.Type
{
    public class ItemsTypeCharacteristics
    {
        public int IdItemType { get; set; }
        public int IdTypeCharacteristic { get; set; }
        public string? TypeCharacteristicValue { get; set; }
        public ItemsType ItemType { get; set; }
        public TypeCharacteristics TypeCharacteristic { get; set; }
    }
}
