
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces
{
    public interface IDealsService
    {
        Task<DealReadDto> GetDealById(int id);
        Task<IEnumerable<DealOffersReadDto>> GetDealOffers(int id);
        Task DeleteDeal(int id, string uuid);
        Task CreateDeal(DealCreateDto dealCreateDto, string uuid);
        Task AcceptDealOffer(int dealId, int offerId, string sellerUUID);
    }
}
