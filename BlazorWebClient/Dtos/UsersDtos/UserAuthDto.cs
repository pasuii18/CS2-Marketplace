using System.ComponentModel.DataAnnotations;

namespace BlazorWebClient.Dtos.UsersDtos
{
    public class UserAuthDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string? Password { get; set; }
    }
}