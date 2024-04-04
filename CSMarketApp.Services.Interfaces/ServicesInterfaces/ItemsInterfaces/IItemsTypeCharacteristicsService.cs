
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsTypeCharacteristics;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsTypeCharacteristicsService
    {
        Task<IEnumerable<ItemsTypeCharacteristicsReadDto>> GetAll();
        Task Create(ItemsTypeCharacteristicsCreateDto createDto);
        Task Update(ItemsTypeCharacteristicsUpdateDto updateDto);
        Task Delete(ItemsTypeCharacteristicsDeleteDto deleteDto);
    }
}
