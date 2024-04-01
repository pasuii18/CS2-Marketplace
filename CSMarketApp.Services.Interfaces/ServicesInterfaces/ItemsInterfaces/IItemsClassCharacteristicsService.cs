
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClassCharacteristics;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsClassCharacteristicsService
    {
        Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetAll();
        Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetByItemId(int id);
        Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetByCharacteristicId(int id);
        Task Create(ItemsClassCharacteristicsCreateDto createDto);
        Task Update(ItemsClassCharacteristicsUpdateDto updateDto);
        Task Delete(ItemsClassCharacteristicsDeleteDto deleteDto);
    }
}
