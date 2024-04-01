using BlazorWebClient.Dtos.DealsDtos.DealHistory;

namespace BlazorWebClient.Dtos.DealsDtos.DealHistory
{
    public class UserDealsHistoryReadDto
    {
        public ItemDealsHistoryReadDto Item { get; set; }
        public string SellerUUID { get; set; }
        public string BuyerUUID { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
