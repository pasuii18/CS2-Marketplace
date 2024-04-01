namespace CSMarketApp.Services.Interfaces.Dtos.ItemsDtos
{
    public class ItemDealsReadDto
    {
        public int IdDeal { get; set; }
        public int? IdUserProfilePicture { get; set; }
        public string? UUID { get; set; }
        public string? Username { get; set; }
        public decimal? Price { get; set; }
    }
}
