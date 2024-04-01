using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClass;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.Class
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class ItemsClassController : Controller
    {
        private readonly ITemplateService<ItemsClassReadDto, ItemsClassCreateDto, ItemsClassUpdateDto> _service;

        public ItemsClassController(ITemplateService<ItemsClassReadDto, ItemsClassCreateDto, ItemsClassUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsClassReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        // [HttpGet("{id}")]
        // public async Task<ActionResult<ItemsClassReadDto>> GetById(int id)
        // {
        //     return Ok(await _service.GetById(id));
        // }
        [HttpPost]
        public async Task<ActionResult> Create(ItemsClassCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(ItemsClassUpdateDto updateDto)
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
