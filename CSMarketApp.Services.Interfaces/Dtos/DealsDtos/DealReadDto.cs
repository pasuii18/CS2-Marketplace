
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type;

namespace CSMarketApp.Services.Interfaces.Dtos.DealsDtos
{
    public class DealReadDto
    {
        //public int? IdItemPicture { get; set; }
        //public string? SkinName { get; set; }
        //public int? Rarity { get; set; }
        //public string? ItemTypeName { get; set; }
        //public List<TypeCharacteristicsValuesReadDto> ItemsTypeCharacteristics { get; set; }
        //public string? ItemClassName { get; set; }
        //public List<ClassCharacteristicsValuesReadDto> ItemsClassCharacteristics { get; set; }
        //public string? ItemSubClassName { get; set; }
        //public int? IdUserPicture { get; set; }
        //public string? UserUUID { get; set; }
        //public string? Username { get; set; }
        //public decimal? Price { get; set; }
        public int IdDeal { get; set; }
        public int IdItem { get; set; }
        public int? IdItemPicture { get; set; }
        public string? SkinName { get; set; }
        public int? Rarity { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemClassName { get; set; }
        public string? ItemSubClassName { get; set; }
        public int? IdUserPicture { get; set; }
        public string? UserUUID { get; set; }
        public string? Username { get; set; }
        public decimal? Price { get; set; }
    }
}
