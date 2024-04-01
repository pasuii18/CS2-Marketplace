using Microsoft.AspNetCore.Http;

namespace CSMarketApp.Services.Interfaces.Dtos.UsersDtos
{
    public class UserUpdateDto
    {
        public int? IdUserProfilePicture { get; set; }
        public string? Username { get; set; }
        public string? NewLogin { get; set; }
        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
        public string? Description { get; set; }
        public IFormFile? UserPicture { get; set; }
    }
}
