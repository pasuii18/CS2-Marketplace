using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos
{
    public class ItemsRepo : IItemsRepo
    {
        private readonly AppDbContext _context;

        public ItemsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateItem(Items item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await _context.AddAsync(item);
            await SaveChanges();
            return item.IdItem;
        }

        public void DeleteItem(Items row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            _context.Remove(row);
        }

        public async Task<IEnumerable<Items>> GetAllItems()
        {
            return await _context.Items
                .Include(i => i.ItemPicture)
                .Include(i => i.Skin)
                .Include(i => i.ItemsType)
                .Include(i => i.ItemsType.ItemsClass)
                .Include(i => i.ItemsType.ItemsClass.ItemsSubClass)
                .Include(i => i.Deals)
                .ToListAsync();
        }

        public async Task<Items?> GetItemById(int id)
        {
            return await _context.Items
                .Include(item => item.ItemPicture)
                .Include(item => item.Skin)
                .Include(item => item.Deals)
                    .ThenInclude(deals => deals.User)
                .Include(item => item.ItemsType)
                    .ThenInclude(itemType => itemType.ItemsTypeCharacteristics)
                        .ThenInclude(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(item => item.ItemsType.ItemsClass)
                    .ThenInclude(itemClass => itemClass.ItemsClassCharacteristics)
                        .ThenInclude(itemsClassCharacteristics => itemsClassCharacteristics.ClassCharacteristic)
                .Include(item => item.ItemsType.ItemsClass.ItemsSubClass)
                .Where(item => item.IdItem == id)
                .FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
