namespace CSMarketApp.Domain.Core.Models.ItemsModels
{
    public class ItemsPictures
    {
        public int IdItemPicture { get; set; }
        public string ItemPicturePath { get; set; }
        public Items? Item { get; set; }
    }
}
