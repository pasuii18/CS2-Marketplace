using BlazorWebClient.Dtos.ItemsDtos;
using BlazorWebClient.Dtos.ItemsDtos.Class;
using BlazorWebClient.Dtos.ItemsDtos.Type;

namespace BlazorWebClient.Dtos.ItemsDtos
{
    public class ItemDetailedReadDto
    {
        public int IdItem { get; set; }
        public int? IdItemPicture { get; set; }
        public string? SkinName { get; set; }
        public int? Rarity { get; set; }
        public string? ItemTypeName { get; set; }
        public List<TypeCharacteristicsValuesReadDto> ItemsTypeCharacteristics { get; set; }
        public string? ItemClassName { get; set; }
        public List<ClassCharacteristicsValuesReadDto> ItemsClassCharacteristics { get; set; }
        public string? ItemSubClassName { get; set; }
        public List<ItemDealsReadDto> ItemDeals { get; set; }
    }
}
