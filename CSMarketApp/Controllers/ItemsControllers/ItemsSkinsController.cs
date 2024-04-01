using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Skins;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CSMarketApp.Controllers.ItemsControllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Developer, Administrator")]
public class ItemsSkinsController : Controller
{
    private readonly IItemsSkinsService _itemsSkinsService;

    public ItemsSkinsController(IItemsSkinsService itemsSkinsService)
    {
        _itemsSkinsService = itemsSkinsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemsReadDto>>> GetAllSkins()
    {
        return Ok(await _itemsSkinsService.GetAllSkins());
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<ItemDetailedReadDto>> GetSkinById(int id)
    // {
    //     return Ok(await _itemsSkinsService.GetById(id));
    // }

    [HttpPost]
    public async Task<ActionResult<string>> Create(SkinsCreateDto createDto)
    {
        await _itemsSkinsService.Create(createDto);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<IEnumerable<DealHistoryReadDto>>> Update(SkinsUpdateDto updateDto)
    {
        await _itemsSkinsService.Update(updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _itemsSkinsService.Delete(id);
        return NoContent();
    }
}