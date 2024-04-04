using CSMarketApp.Domain.Core.Models.DealsModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces
{
    public interface IDealOffersRepo
    {
        Task<IEnumerable<DealOffers>> GetAllByDealId(int id);
        Task<IEnumerable<DealOffers>> GetAllByUserId(int id);
        Task<DealOffers> GetOfferById(int id);
        Task Create(DealOffers record);
        void Delete(DealOffers record);
        Task SaveChanges();
    }
}
