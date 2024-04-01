namespace BlazorWebClient.Dtos.DealsDtos.DealHistory
{
    public class DealsHistoryCreateDto
    {
        public int IdItem { get; set; }
        public int IdSeller { get; set; }
        public int IdBuyer { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}
