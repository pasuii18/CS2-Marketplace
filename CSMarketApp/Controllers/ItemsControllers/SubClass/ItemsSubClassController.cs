using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.SubClass.ItemsSubClass;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.SubClass
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class ItemsSubClassController : Controller
    {
        private readonly ITemplateService<ItemsSubClassReadDto, ItemsSubClassCreateDto, ItemsSubClassUpdateDto> _service;

        public ItemsSubClassController(ITemplateService<ItemsSubClassReadDto, ItemsSubClassCreateDto, ItemsSubClassUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsSubClassReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        // [HttpGet("{id}")]
        // public async Task<ActionResult<ItemsSubClassReadDto>> GetById(int id)
        // {
        //     return Ok(await _service.GetById(id));
        // }
        [HttpPost]
        public async Task<ActionResult> Create(ItemsSubClassCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(ItemsSubClassUpdateDto updateDto)
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
