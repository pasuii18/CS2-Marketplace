
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces
{
    public interface IDealsHistoryService
    {
        Task<IEnumerable<DealHistoryReadDto>> GetAllHistory();
    }
}
