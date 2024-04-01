using System.ComponentModel.DataAnnotations;

namespace BlazorWebClient.Dtos.UsersDtos
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Login is required.")]
        [MinLength(4, ErrorMessage = "Login must be at least 4 characters long.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one letter and one digit.")]
        public string Password { get; set; }
    }
}
