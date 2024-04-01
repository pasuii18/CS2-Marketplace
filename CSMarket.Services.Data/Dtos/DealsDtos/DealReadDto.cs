namespace CSMarketApp.Dtos.DealsDtos
{
    public class DealReadDto
    {
        //public int IdItem { get; set; }
        //public int IdUser { get; set; }
        public string? ItemTypeName { get; set; }
        public List<ItemsTypeCharacteristicsReadDto> ItemsTypeCharacteristics { get; set; }
        public string? ItemClassName { get; set; }
        public List<ItemsClassCharacteristicsReadDto> ItemsClassCharacteristics { get; set; }
        public string? ItemSubClassName { get; set; }
        public string? Username { get; set; }
        public decimal? Price { get; set; }
    }
}
