using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.TypeCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.Type
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class TypeCharacteristicsController : Controller
    {
        private readonly ITemplateService<TypeCharacteristicsReadDto, TypeCharacteristicsCreateDto, TypeCharacteristicsUpdateDto> _service;

        public TypeCharacteristicsController(ITemplateService<TypeCharacteristicsReadDto, TypeCharacteristicsCreateDto, TypeCharacteristicsUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeCharacteristicsReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> Create(TypeCharacteristicsCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(TypeCharacteristicsUpdateDto updateDto)
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
