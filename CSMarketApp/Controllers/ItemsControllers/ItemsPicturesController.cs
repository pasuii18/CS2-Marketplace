using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsPicturesController : Controller
    {
        private readonly IItemsPicturesService _service;

        public ItemsPicturesController(IItemsPicturesService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            byte[] fileBytes = await _service.GetById(id);
            return File(fileBytes, "image/png");
        }
    }
}
