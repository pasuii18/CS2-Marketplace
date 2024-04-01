using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace BlazorWebClient.Dtos.UsersDtos
{
    public class UserUpdateDto
    {
        public int? IdUserProfilePicture { get; set; }
        public string? Username { get; set; }
        public string? NewLogin { get; set; }
        public string? NewPassword { get; set; }
        public string? OldPassword { get; set; }
        public string? Description { get; set; }
        //public IFormFile? UserPicture { get; set; }
        public IBrowserFile? UserPicture { get; set; }
    }
}
