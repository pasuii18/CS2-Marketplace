using Microsoft.AspNetCore.Http;

namespace CSMarketApp.Services.Interfaces.Dtos.ItemsDtos
{
    public class ItemCreateDto
    {
        public int? IdItemType { get; set; }
        public int? IdSkin { get; set; }
        public int? Rarity { get; set; }
        public IFormFile? ItemPicture { get; set; }
    }
}
