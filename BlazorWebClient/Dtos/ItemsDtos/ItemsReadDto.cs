namespace BlazorWebClient.Dtos.ItemsDtos
{
    public class ItemsReadDto
    {
        public int IdItem { get; set; }
        public int? IdItemPicture { get; set; }
        public string? SkinName { get; set; }
        public int? Rarity { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemClassName { get; set; }
        public string? ItemSubClassName { get; set; }
        public decimal? LowestPrice { get; set; }
    }
}
