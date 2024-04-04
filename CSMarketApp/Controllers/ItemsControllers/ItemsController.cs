using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CSMarketApp.Controllers.ItemsControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsReadDto>>> GetAllItems()
        {
            return Ok(await _itemsService.GetAllItems());
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public async Task<ActionResult<ItemDetailedReadDto>> GetItemById(int id)
        {
            return Ok(await _itemsService.GetItemById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Developer, Administrator")]
        public async Task<ActionResult<string>> CreateItem([FromForm] ItemCreateSpecialDtoShort formDto)
        {
            var itemCreateDto = JsonConvert.DeserializeObject<ItemCreateSpecialDto>(formDto.ItemJson);
            itemCreateDto.ItemPicture = formDto.ItemPicture;
            var result = await _itemsService.CreateItem(itemCreateDto);

            return Created(nameof(CreateItem), result);
        }

        [HttpGet("{id}/history")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DealHistoryReadDto>>> GetAllItemHistory(int id)
        {
            return Ok(await _itemsService.GetAllItemHistory(id));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Developer, Administrator")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _itemsService.DeleteItem(id);
            return NoContent();
        }
    }
}
