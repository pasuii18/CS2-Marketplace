using System.ComponentModel.DataAnnotations;

namespace BlazorWebClient.Dtos.UsersDtos
{
    public class UserAuthDto
    {
        [Required(ErrorMessage = "Login is required.")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}