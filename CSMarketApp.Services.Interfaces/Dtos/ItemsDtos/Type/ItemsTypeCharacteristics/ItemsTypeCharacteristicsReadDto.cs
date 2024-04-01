namespace CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsTypeCharacteristics
{
    public class ItemsTypeCharacteristicsReadDto
    {
        public int IdItemType { get; set; }
        public int IdTypeCharacteristic { get; set; }
        public string ItemTypeName { get; set; }
        public string ItemClassName { get; set; }
        public string ItemSubClassName { get; set; }
        public string TypeCharacteristicName { get; set; }
        public string? TypeCharacteristicValue { get; set; }
    }
}
