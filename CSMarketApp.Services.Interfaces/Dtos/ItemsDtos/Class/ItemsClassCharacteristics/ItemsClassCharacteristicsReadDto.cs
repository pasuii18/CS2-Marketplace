namespace CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClassCharacteristics
{
    public class ItemsClassCharacteristicsReadDto
    {
        public int IdItemClass { get; set; }
        public int IdClassCharacteristic { get; set; }
        public string ItemClassName { get; set; }
        public string ItemSubClassName { get; set; }
        public string ClassCharacteristicName { get; set; }
        public string? ClassCharacteristicValue { get; set; }
    }
}
