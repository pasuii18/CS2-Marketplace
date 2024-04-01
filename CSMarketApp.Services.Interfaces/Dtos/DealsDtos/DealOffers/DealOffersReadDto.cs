namespace CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers
{
    public class DealOffersReadDto
    {
        public int IdDealOffer { get; set; }
        public int IdDeal { get; set; }
        public string OffererUUID { get; set; }
        public decimal OfferPrice { get; set; }
    }
}
