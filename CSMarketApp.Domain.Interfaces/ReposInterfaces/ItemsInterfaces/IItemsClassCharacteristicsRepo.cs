using CSMarketApp.Domain.Core.Models.ItemsModels.Class;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces
{
    public interface IItemsClassCharacteristicsRepo
    {
        Task<IEnumerable<ItemsClassCharacteristics>> GetAll();
        Task<ItemsClassCharacteristics> GetByIds(int idItem, int idCharacteristic);
        Task<IEnumerable<ItemsClassCharacteristics>> GetByItemId(int id);
        Task<IEnumerable<ItemsClassCharacteristics>> GetByCharacteristicId(int id);
        Task Create(ItemsClassCharacteristics record);
        void Delete(ItemsClassCharacteristics record);
        Task SaveChanges();
    }
}
