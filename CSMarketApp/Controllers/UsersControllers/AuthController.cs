using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.UsersControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Authentication(UserAuthDto userAuthDto)
        {
            return Ok(await _authService.Authentication(userAuthDto));
        }

        [HttpPost("registration")]
        public async Task<ActionResult<string>> Registration(UserCreateDto userCreateDto)
        {
            return Created(nameof(Registration), await _authService.Registration(userCreateDto));
        }
    }
}
