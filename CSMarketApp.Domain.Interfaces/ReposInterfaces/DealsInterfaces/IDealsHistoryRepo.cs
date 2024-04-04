using CSMarketApp.Domain.Core.Models.DealsModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces
{
    public interface IDealsHistoryRepo
    {
        Task<IEnumerable<DealsHistory>> GetAllItemHistory(int idItem);
        Task<IEnumerable<DealsHistory>> GetAllUserSellingHistory(int IdSeller);
        Task<IEnumerable<DealsHistory>> GetAllUserBuyingHistory(int idBuyer);
        Task CreateRecord(DealsHistory record);
        Task SaveChanges();
    }
}
