using BlazorWebClient.Dtos.ItemsDtos.Class;
using BlazorWebClient.Dtos.ItemsDtos.Type;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorWebClient.Dtos.ItemsDtos
{
    public class ItemCreateSpecialDto
    {
        public string SkinName { get; set; }
        public int Rarity { get; set; }
        public string ItemTypeName { get; set; }

        public List<TypeCharacteristicsValuesReadDto> ItemTypeCharacteristics { get; set; } =
            new List<TypeCharacteristicsValuesReadDto>();
        public string ItemClassName { get; set; }
        public List<ClassCharacteristicsValuesReadDto> ItemClassCharacteristics { get; set; } =
            new List<ClassCharacteristicsValuesReadDto>();
        public string ItemSubClassName { get; set; }
        public IFormFile? ItemPicture { get; set; }
    }
    public class ItemCreateSpecialDtoShort
    {
        public string ItemJson { get; set; }
        public IBrowserFile? ItemPicture { get; set; }
    }
}
