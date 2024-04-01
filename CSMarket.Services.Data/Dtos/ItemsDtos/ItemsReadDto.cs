namespace CSMarketApp.Dtos.ItemsDtos
{
    public class ItemsReadDto
    {
        public string? ItemTypeName { get; set; }
        public string? ItemClassName { get; set; }
        public string? ItemSubClassName { get; set; }
        //public string? ItemName { get; set; }
        public decimal? LowestPrice { get; set; }
    }
}
