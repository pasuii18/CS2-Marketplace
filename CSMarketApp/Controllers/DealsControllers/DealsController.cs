using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.DealsControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class DealsController : Controller
    {
        private readonly IDealsService _service;

        public DealsController(IDealsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DealReadDto>> GetDealById(int id)
        {
            return Ok(await _service.GetDealById(id));
        }

        [HttpGet("{id}/offers")]
        public async Task<ActionResult<DealOffersReadDto>> GetDealOffers(int id)
        {
            return Ok(await _service.GetDealOffers(id));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult> CreateDeal(DealCreateDto dealCreateDto)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                return Unauthorized("UUID not found in token claims.");
            }

            await _service.CreateDeal(dealCreateDto, uuid);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteDeal(int id)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                return Unauthorized("UUID not found in token claims.");
            }

            await _service.DeleteDeal(id, uuid);
            return NoContent();
        }

        [HttpPost("{dealId}/offers/{offerId}/accept")]
        [Authorize]
        public async Task<ActionResult> AcceptDealOffer(int dealId, int offerId)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                return Unauthorized("UUID not found in token claims.");
            }
            await _service.AcceptDealOffer(dealId, offerId, uuid);
            return Ok();
        }
    }
}
