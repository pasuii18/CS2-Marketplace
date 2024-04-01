using CSMarketApp.Domain.Core.Models.ItemsModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces
{
    public interface IItemsSkinsRepo
    {
        Task<IEnumerable<Skins>> GetAllSkins();
        Task<Skins> GetById(int id);
        Task<Skins> GetByName(string name);
        Task<int> GetOrCreate(Skins model);
        Task<int> Create(Skins record);
        void Delete(Skins record);
        Task SaveChanges();
    }
}
