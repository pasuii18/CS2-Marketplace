using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;

namespace CSMarketApp.Domain.Core.Models.ItemsModels
{
    public class Items
    {
        public int IdItem { get; set; }
        public int? IdItemPicture { get; set; }
        public int IdSkin { get; set; }
        public int IdItemType { get; set; }
        public int? Rarity { get; set; }

        public ItemsPictures ItemPicture { get; set; }
        public Skins Skin { get; set; }
        public ItemsType ItemsType { get; set; }
        public List<Deals> Deals { get; set; }
        public List<DealsHistory> DealsHistory { get; set; }
    }
}