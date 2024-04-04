using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.DealsRepos
{
    public class DealOffersRepo : IDealOffersRepo
    {
        private readonly AppDbContext _context;

        public DealOffersRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DealOffers>> GetAllByDealId(int id)
        {
            return await _context.DealOffers
                .Include(dealOffer => dealOffer.Offerer)
                .Where(record => record.IdDeal == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<DealOffers>> GetAllByUserId(int id)
        {
            return await _context.DealOffers
                .Include(dealOffer => dealOffer.Offerer)
                .Where(record => record.IdOfferer == id)
                .ToListAsync();
        }

        public async Task<DealOffers> GetOfferById(int id)
        {
            return await _context.DealOffers
                .Include(dealOffer => dealOffer.Offerer)
                .FirstOrDefaultAsync(record => record.IdDealOffer == id);
        }
        public async Task Create(DealOffers record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
        }
        public void Delete(DealOffers record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            _context.Remove(record);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
