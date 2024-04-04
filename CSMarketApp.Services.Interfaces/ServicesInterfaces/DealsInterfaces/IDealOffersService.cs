
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces
{
    public interface IDealOffersService
    {
        Task<IEnumerable<DealOffersReadDto>> GetAllByDealId(int id);
        Task<string> Create(DealOffersCreateDto createDto, string uuid);
        Task Update(DealOffersUpdateDto updateDto, string uuid);
        Task Delete(int id, string uuid);
    }
}
