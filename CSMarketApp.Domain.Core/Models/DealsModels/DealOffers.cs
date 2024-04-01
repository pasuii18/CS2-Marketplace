using CSMarketApp.Domain.Core.Models.UsersModels;

namespace CSMarketApp.Domain.Core.Models.DealsModels
{
    public class DealOffers
    {
        public int IdDealOffer { get; set; }
        public int IdDeal { get; set; }
        public int IdOfferer { get; set; }
        public decimal OfferPrice { get; set; }
        public Deals? Deal { get; set; }
        public Users? Offerer { get; set; }
    }
}
