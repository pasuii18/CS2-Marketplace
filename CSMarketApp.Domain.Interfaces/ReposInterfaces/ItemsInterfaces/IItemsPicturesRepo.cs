using CSMarketApp.Domain.Core.Models.ItemsModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces
{
    public interface IItemsPicturesRepo
    {
        Task<IEnumerable<ItemsPictures>> GetAll();
        Task<ItemsPictures?> GetById(int id);
        Task<int> Create(ItemsPictures record);
        void Delete(ItemsPictures record);
        Task SaveChanges();
    }
}
