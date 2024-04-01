using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsType;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.Type
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class ItemsTypeController : Controller
    {
        private readonly ITemplateService<ItemsTypeReadDto, ItemsTypeCreateDto, ItemsTypeUpdateDto> _service;

        public ItemsTypeController(ITemplateService<ItemsTypeReadDto, ItemsTypeCreateDto, ItemsTypeUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsTypeReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        // [HttpGet("{id}")]
        // public async Task<ActionResult<ItemsTypeReadDto>> GetById(int id)
        // {
        //     return Ok(await _service.GetById(id));
        // }
        [HttpPost]
        public async Task<ActionResult<RecordIdReadDto>> Create(ItemsTypeCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(ItemsTypeUpdateDto updateDto)
        {
            await _service.Update(updateDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
