using CSMarketApp.Domain.Core.Models.DealsModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces
{
    public interface IDealsRepo
    {
        Task SaveChanges();
        Task<Deals?> GetRowById(int id);
        Task<IEnumerable<Deals>> GetUserRows(int id);
        Task<IEnumerable<Deals?>> GetItemRows(int id);
        Task CreateRow(Deals row);
        void DeleteRow(Deals row);
    }
}
