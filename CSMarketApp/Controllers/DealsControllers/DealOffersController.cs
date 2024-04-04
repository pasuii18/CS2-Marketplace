using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.DealsControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DealOffersController : Controller
    {
        private readonly IDealOffersService _service;

        public DealOffersController(IDealOffersService itemsService)
        {
            _service = itemsService;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DealOffersReadDto>>> GetAllByDealId(int id)
        {
            return Ok(await _service.GetAllByDealId(id));
        }
        [HttpPost]
        public async Task<ActionResult> Create(DealOffersCreateDto createDto)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                return Unauthorized("UUID not found in token claims.");
            }

            await _service.Create(createDto, uuid);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(DealOffersUpdateDto updateDto)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                return Unauthorized("UUID not found in token claims.");
            }
            
            await _service.Update(updateDto, uuid);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                return Unauthorized("UUID not found in token claims.");
            }
            
            await _service.Delete(id, uuid);
            return NoContent();
        }
    }
}
