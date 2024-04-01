namespace CSMarketApp.Services.Interfaces.Dtos.UsersDtos
{
    public class UserProfileDto
    {
        public string? UUID { get; set; }
        public int IdPicture { get; set; }
        public string? Role { get; set; }
        public string? Username { get; set; }
        public string? Description { get; set; }
    }
}
