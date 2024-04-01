namespace CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsType
{
    public class ItemsTypeReadDto
    {
        public int IdItemType { get; set; }
        public int IdItemClass { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemClassName { get; set; }
        public string? ItemSubClasName { get; set; }
    }
}
