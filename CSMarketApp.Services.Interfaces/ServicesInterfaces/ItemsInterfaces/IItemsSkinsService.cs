using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Skins;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;

public interface IItemsSkinsService
{
    public Task<IEnumerable<SkinsReadDto>> GetAllSkins();
    public Task Create(SkinsCreateDto record);
    public Task Update(SkinsUpdateDto record);
    public Task Delete(int id);
}