using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ClassCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.ItemsControllers.Class
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class ClassCharacteristicsController : Controller
    {
        private readonly ITemplateService<ClassCharacteristicsReadDto, ClassCharacteristicsCreateDto, ClassCharacteristicsUpdateDto> _service;

        public ClassCharacteristicsController(ITemplateService<ClassCharacteristicsReadDto, ClassCharacteristicsCreateDto, ClassCharacteristicsUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassCharacteristicsReadDto>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClassCharacteristicsCreateDto createDto)
        {
            await _service.Create(createDto);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(ClassCharacteristicsUpdateDto updateDto)
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
