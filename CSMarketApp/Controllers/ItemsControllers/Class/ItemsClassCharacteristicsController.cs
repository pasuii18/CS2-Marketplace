using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClassCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.Class
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class ItemsClassCharacteristicsController : Controller
    {
        private readonly IItemsClassCharacteristicsService _service;

        public ItemsClassCharacteristicsController(IItemsClassCharacteristicsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsClassCharacteristicsReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        // [HttpGet("item/{id}")]
        // public async Task<ActionResult<ItemsClassCharacteristicsReadDto>> GetByItemId(int id)
        // {
        //     return Ok(await _service.GetByItemId(id));
        // }
        // [HttpGet("characteristic/{id}")]
        // public async Task<ActionResult<ItemsClassCharacteristicsReadDto>> GetByCharacteristicId(int id)
        // {
        //     return Ok(await _service.GetByCharacteristicId(id));
        // }
        [HttpPost]
        public async Task<ActionResult> Create(ItemsClassCharacteristicsCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(ItemsClassCharacteristicsUpdateDto updateDto)
        {
            await _service.Update(updateDto);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(ItemsClassCharacteristicsDeleteDto deleteDto)
        {
            await _service.Delete(deleteDto);
            return NoContent();
        }
    }
}
