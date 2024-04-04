
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClassCharacteristics;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsClassCharacteristicsService
    {
        Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetAll();
        Task Create(ItemsClassCharacteristicsCreateDto createDto);
        Task Update(ItemsClassCharacteristicsUpdateDto updateDto);
        Task Delete(ItemsClassCharacteristicsDeleteDto deleteDto);
    }
}
