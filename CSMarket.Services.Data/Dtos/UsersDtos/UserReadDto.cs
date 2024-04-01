namespace CSMarketApp.Dtos.UsersDtos
{
    public class UserReadDto
    {
        public int IdUser { get; set; }
        public string? UUID { get; set; }
        public string? Username { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; }
    }
}