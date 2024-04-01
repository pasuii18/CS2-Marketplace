namespace BlazorWebClient.Dtos.DealsDtos.DealHistory
{
    public class DealHistoryReadDto
    {
        public int IdItem { get; set; }
        public string SellerUUID { get; set; }
        public string BuyerUUID { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
