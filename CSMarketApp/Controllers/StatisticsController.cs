using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IStatisticService _service;

        public StatisticsController(IStatisticService service)
        {
            _service = service;
        }

        [HttpGet("topUserBuyings")]
        public async Task<ActionResult<IEnumerable<UsersBuyingsSumDto>>> GetTopOfUsersByBuyings()
        {
            return Ok(await _service.GetTopOfUsersByBuyings());
        }

        [HttpGet("topSellingItem")]
        public async Task<ActionResult<ItemSellingsDto>> GetTopSellingItem()
        {
            return Ok(await _service.GetTopSellingItem());
        }

        [HttpGet("topWorstUsers")]
        public async Task<ActionResult<IEnumerable<UserStatDto>>> GetTopWorstUsers()
        {
            return Ok(await _service.GetTopWorstUsers());
        }
    }
}
