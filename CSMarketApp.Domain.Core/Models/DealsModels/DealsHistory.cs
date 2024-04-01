using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Core.Models.UsersModels;

namespace CSMarketApp.Domain.Core.Models.DealsModels
{
    public class DealsHistory
    {
        public int IdDealsHistory { get; set; }
        public int IdItem { get; set; }
        public int IdSeller { get; set; }
        public int IdBuyer { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public Items? Item { get; set; }
        public Users? Seller { get; set; }
        public Users? Buyer { get; set; }
    }
}