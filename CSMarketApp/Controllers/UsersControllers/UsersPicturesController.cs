using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Pictures;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.UsersControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersPicturesController : ControllerBase
    {
        private readonly IUsersPicturesService _service;

        public UsersPicturesController(IUsersPicturesService service)
        {
            _service = service;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            byte[] fileBytes = await _service.GetById(id);
            return File(fileBytes, "image/jpeg");
        }
    }
}
