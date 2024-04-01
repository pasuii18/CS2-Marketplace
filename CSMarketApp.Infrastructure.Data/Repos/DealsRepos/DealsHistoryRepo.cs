using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.DealsRepos
{
    public class DealsHistoryRepo : IDealsHistoryRepo
    {
        private readonly AppDbContext _context;

        public DealsHistoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DealsHistory>> GetAllHistory()
        {
            return await _context.DealsHistory
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .ToListAsync();
        }
        public async Task<IEnumerable<DealsHistory>> GetAllItemHistory(int idItem)
        {
            return await _context.DealsHistory
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .Where(record => record.IdItem == idItem)
                .ToListAsync();
        }
        public async Task<IEnumerable<DealsHistory>> GetAllUserSellingHistory(int idSeller)
        {
            return await _context.DealsHistory
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .Include(i => i.Item)
                .Include(i => i.Item.ItemsType)
                .Include(i => i.Item.ItemsType.ItemsClass)
                .Include(i => i.Item.ItemsType.ItemsClass.ItemsSubClass)
                .Include(i => i.Item.Skin)
                .Where(record => record.IdSeller == idSeller)
                .ToListAsync();
        }
        public async Task<IEnumerable<DealsHistory>> GetAllUserBuyingHistory(int idBuyer)
        {
            return await _context.DealsHistory
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .Include(i => i.Item)
                .Include(i => i.Item.ItemsType)
                .Include(i => i.Item.ItemsType.ItemsClass)
                .Include(i => i.Item.ItemsType.ItemsClass.ItemsSubClass)
                .Include(i => i.Item.Skin)
                .Where(record => record.IdBuyer == idBuyer)
                .ToListAsync();
        }
        public async Task CreateRecord(DealsHistory record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
