using CSMarketApp.Domain.Core.Models.ItemsModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces
{
    public interface IItemsRepo
    {
        Task SaveChanges();
        Task<Items?> GetItemById(int id);
        Task<IEnumerable<Items>> GetAllItems();
        Task<int> CreateItem(Items row);
        void DeleteItem(Items row);
    }
}
