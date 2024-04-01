using BlazorWebClient.Dtos.ItemsDtos.Class;
using BlazorWebClient.Dtos.ItemsDtos.Type;

namespace BlazorWebClient.Dtos.DealsDtos
{
    public class DealReadDto
    {
        public int IdDeal { get; set; }
        public int IdItem { get; set; }
        public int? IdItemPicture { get; set; }
        public string? SkinName { get; set; }
        public int? Rarity { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemClassName { get; set; }
        public string? ItemSubClassName { get; set; }
        public string? UserUUID { get; set; }
        public string? Username { get; set; }
        public decimal? Price { get; set; }
    }
}
