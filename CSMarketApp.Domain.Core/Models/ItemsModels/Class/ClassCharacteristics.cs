namespace CSMarketApp.Domain.Core.Models.ItemsModels.Class
{
    public class ClassCharacteristics
    {
        public int IdClassCharacteristic { get; set; }
        public string? ClassCharacteristicName { get; set; }
        public List<ItemsClassCharacteristics> ItemsClassCharacteristics { get; set; }
    }
}
