using System.Data;
using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Skins;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices;

public class ItemsSkinsService : IItemsSkinsService
{
    private readonly IItemsSkinsRepo _itemsSkinsRepo;
    private readonly IMapper _mapper;

    public ItemsSkinsService(IItemsSkinsRepo itemsRepo, IMapper mapper)
    {
        _itemsSkinsRepo = itemsRepo;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<SkinsReadDto>> GetAllSkins()
    {
        var records = await _itemsSkinsRepo.GetAllSkins();
        return _mapper.Map<IEnumerable<SkinsReadDto>>(records);
    }
    public async Task<SkinsReadDto> GetById(int id)
    {
        var record = await _itemsSkinsRepo.GetById(id);
        if (record == null)
        {
            throw new KeyNotFoundException("Skin with your ID is not found!");
        }
        return _mapper.Map<SkinsReadDto>(record);
    }
    
    public async Task Update(SkinsUpdateDto updateDto)
    {
        var record = await _itemsSkinsRepo.GetById(updateDto.IdSkin);
        if (record == null)
        {
            throw new KeyNotFoundException("Skin with your ID is not found!");
        }
    
        _mapper.Map(updateDto, record);
        await _itemsSkinsRepo.SaveChanges();
    }
    public async Task Create(SkinsCreateDto createDto)
    {
        var record = await _itemsSkinsRepo.GetByName(createDto.SkinName);
        if (record != null)
        {
            throw new DuplicateNameException("Skin with this name already exists!");
        }
        var model = _mapper.Map<Skins>(createDto);
        await _itemsSkinsRepo.Create(model);
    }
    public async Task Delete(int id)
    {
        var record = await _itemsSkinsRepo.GetById(id);
        if (record == null)
        {
            throw new KeyNotFoundException("ItemSubClass with your ID is not found!");
        }
        _itemsSkinsRepo.Delete(record);
        await _itemsSkinsRepo.SaveChanges();
    }
}