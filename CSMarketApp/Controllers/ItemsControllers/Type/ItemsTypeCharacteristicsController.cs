using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsTypeCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.Type
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class ItemsTypeCharacteristicsController : Controller
    {
        private readonly IItemsTypeCharacteristicsService _service;

        public ItemsTypeCharacteristicsController(IItemsTypeCharacteristicsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsTypeCharacteristicsReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ItemsTypeCharacteristicsCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(ItemsTypeCharacteristicsUpdateDto updateDto)
        {
            await _service.Update(updateDto);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(ItemsTypeCharacteristicsDeleteDto deleteDto)
        {
            await _service.Delete(deleteDto);
            return NoContent();
        }
    }
}
