namespace CSMarketApp.Domain.Core.Models.ItemsModels
{
    public class Skins
    {
        public int IdSkin { get; set; }
        public string SkinName { get; set; }
        public List<Items> Items { get; set; }
    }
}
