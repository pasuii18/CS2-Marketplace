namespace CSMarketApp.Domain.Core.Models.ItemsModels.Type
{
    public class TypeCharacteristics
    {
        public int IdTypeCharacteristic { get; set; }
        public string? TypeCharacteristicName { get; set; }
        public List<ItemsTypeCharacteristics> ItemsTypeCharacteristics { get; set; }
    }
}
