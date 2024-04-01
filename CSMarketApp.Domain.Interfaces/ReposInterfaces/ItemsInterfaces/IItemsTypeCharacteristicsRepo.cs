using CSMarketApp.Domain.Core.Models.ItemsModels.Type;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces
{
    public interface IItemsTypeCharacteristicsRepo
    {
        Task<IEnumerable<ItemsTypeCharacteristics>> GetAll();
        Task<ItemsTypeCharacteristics> GetByIds(int idItem, int idCharacteristic);
        Task<IEnumerable<ItemsTypeCharacteristics>> GetByItemId(int id);
        Task<IEnumerable<ItemsTypeCharacteristics>> GetByCharacteristicId(int id);
        Task Create(ItemsTypeCharacteristics record);
        void Delete(ItemsTypeCharacteristics record);
        Task SaveChanges();
    }
}
