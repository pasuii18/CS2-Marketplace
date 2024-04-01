using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.DealsControllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer, Administrator")]
    public class DealsHistoryController : Controller
    {
        private readonly IDealsHistoryService _service;

        public DealsHistoryController(IDealsHistoryService service)
        {
            _service = service;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<DealHistoryReadDto>>> GetAllHistory()
        // {
        //     return Ok(await _service.GetAllHistory());
        // }
    }
}
