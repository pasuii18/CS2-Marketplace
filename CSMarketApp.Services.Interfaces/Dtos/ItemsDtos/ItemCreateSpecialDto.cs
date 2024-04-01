using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Services.Interfaces.Dtos.ItemsDtos
{
    public class ItemCreateSpecialDto
    {
        public string SkinName { get; set; }
        public int Rarity { get; set; }
        public string ItemTypeName { get; set; }
        public List<TypeCharacteristicsValuesReadDto> ItemTypeCharacteristics { get; set; }
        public string ItemClassName { get; set; }
        public List<ClassCharacteristicsValuesReadDto> ItemClassCharacteristics { get; set; }
        public string ItemSubClassName { get; set; }
        public IFormFile? ItemPicture { get; set; }
    }
    public class ItemCreateSpecialDtoShort
    {
        public string ItemJson { get; set; }
        public IFormFile? ItemPicture { get; set; }
    }

}
