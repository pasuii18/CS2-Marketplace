namespace CSMarketApp.Dtos.ItemsDtos
{
    public class ItemDetailedReadDto
    {
        public string? ItemTypeName { get; set; }
        public List<ItemsTypeCharacteristicsReadDto> ItemsTypeCharacteristics { get; set; }
        public string? ItemClassName { get; set; }
        public List<ItemsClassCharacteristicsReadDto> ItemsClassCharacteristics { get; set; }
        public string? ItemSubClassName { get; set; }
        public List<ItemDealsReadDto> ItemDeals { get; set; }
    }
}
