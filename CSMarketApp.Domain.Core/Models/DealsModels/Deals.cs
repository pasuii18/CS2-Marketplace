using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Core.Models.UsersModels;

namespace CSMarketApp.Domain.Core.Models.DealsModels
{
    public class Deals
    {
        public int IdDeal { get; set; }
        public int IdItem { get; set; }
        public int IdUser { get; set; }
        public decimal Price { get; set; }
        public Items? Item { get; set; }
        public Users? User { get; set; }
        public List<DealOffers> DealOffers { get; set; }
    }
}