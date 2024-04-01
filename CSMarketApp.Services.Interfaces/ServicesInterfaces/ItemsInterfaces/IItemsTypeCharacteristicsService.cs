
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsTypeCharacteristics;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsTypeCharacteristicsService
    {
        Task<IEnumerable<ItemsTypeCharacteristicsReadDto>> GetAll();
        Task<IEnumerable<ItemsTypeCharacteristicsReadDto>> GetByItemId(int id);
        Task<IEnumerable<ItemsTypeCharacteristicsReadDto>> GetByCharacteristicId(int id);
        Task Create(ItemsTypeCharacteristicsCreateDto createDto);
        Task Update(ItemsTypeCharacteristicsUpdateDto updateDto);
        Task Delete(ItemsTypeCharacteristicsDeleteDto deleteDto);
    }
}
