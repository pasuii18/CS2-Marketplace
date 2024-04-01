using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.DealsRepos
{
    public class DealsRepo : IDealsRepo
    {
        private readonly AppDbContext _context;

        public DealsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Deals>> GetAllRows()
        {
            return await _context.Deals
                .Include(d => d.User)
                .Include(item => item.Item.Skin)
                .Include(item => item.Item.ItemPicture)
                .Include(d => d.Item)
                    .ThenInclude(item => item.ItemsType)
                        .ThenInclude(itemType => itemType.ItemsTypeCharacteristics)
                            .ThenInclude(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(d => d.Item.ItemsType)
                    .ThenInclude(itemType => itemType.ItemsClass)
                        .ThenInclude(itemClass => itemClass.ItemsClassCharacteristics)
                            .ThenInclude(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristic)
                .Include(d => d.Item.ItemsType.ItemsClass.ItemsSubClass)
                .ToListAsync();
        }
        public async Task<Deals?> GetRowById(int id)
        {
            return await _context.Deals
                .Include(d => d.User)
                .Include(item => item.Item.Skin)
                .Include(item => item.Item.ItemPicture)
                .Include(d => d.Item)
                    .ThenInclude(item => item.ItemsType)
                        .ThenInclude(itemType => itemType.ItemsTypeCharacteristics)
                            .ThenInclude(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(d => d.Item.ItemsType)
                    .ThenInclude(itemType => itemType.ItemsClass)
                        .ThenInclude(itemClass => itemClass.ItemsClassCharacteristics)
                            .ThenInclude(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristic)
                .Include(d => d.Item.ItemsType.ItemsClass.ItemsSubClass)
                .Where(item => item.IdDeal == id)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Deals?>> GetUserRows(int id)
        {
            return await _context.Deals
                .Include(d => d.User)
                    .ThenInclude(d => d.UserProfilePicture)
                .Include(item => item.Item.Skin)
                .Include(item => item.Item.ItemPicture)
                .Include(d => d.Item)
                    .ThenInclude(item => item.ItemsType)
                .Include(d => d.Item.ItemsType)
                    .ThenInclude(itemType => itemType.ItemsClass)
                .Include(d => d.Item.ItemsType.ItemsClass.ItemsSubClass)
                .Where(item => item.IdUser == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<Deals?>> GetItemRows(int id)
        {
            return await _context.Deals
                .Include(d => d.User)
                    .ThenInclude(d => d.UserProfilePicture)
                .Include(item => item.Item.Skin)
                .Include(item => item.Item.ItemPicture)
                .Include(d => d.Item)
                    .ThenInclude(item => item.ItemsType)
                        .ThenInclude(itemType => itemType.ItemsTypeCharacteristics)
                            .ThenInclude(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(d => d.Item.ItemsType)
                    .ThenInclude(itemType => itemType.ItemsClass)
                        .ThenInclude(itemClass => itemClass.ItemsClassCharacteristics)
                            .ThenInclude(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristic)
                .Include(d => d.Item.ItemsType.ItemsClass.ItemsSubClass)
                .Where(item => item.IdItem == id)
                .ToListAsync();
        }
        public async Task CreateRow(Deals row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            await _context.AddAsync(row);
        }
        public void DeleteRow(Deals row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            _context.Remove(row);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
