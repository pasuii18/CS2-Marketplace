using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Type
{
    public class ItemsTypeCharacteristicsRepo : IItemsTypeCharacteristicsRepo
    {
        private readonly AppDbContext _context;

        public ItemsTypeCharacteristicsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemsTypeCharacteristics>> GetAll()
        {
            return await _context.ItemsTypeCharacteristics
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.ItemType)
                .ThenInclude(itemType => itemType.ItemsClass)
                .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .ToListAsync();
        }
        
        public async Task<ItemsTypeCharacteristics> GetByIds(int idItem, int idCharacteristic)
        {
            return await _context.ItemsTypeCharacteristics
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.ItemType)
                    .ThenInclude(itemType => itemType.ItemsClass)
                        .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .Where(record => record.IdItemType == idItem && record.IdTypeCharacteristic == idCharacteristic)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ItemsTypeCharacteristics>> GetByItemId(int id)
        {
            return await _context.ItemsTypeCharacteristics
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.ItemType)
                .ThenInclude(itemType => itemType.ItemsClass)
                .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .Where(record => record.IdItemType == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<ItemsTypeCharacteristics>> GetByCharacteristicId(int id)
        {
            return await _context.ItemsTypeCharacteristics
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.TypeCharacteristic)
                .Include(itemTypeCharacteristics => itemTypeCharacteristics.ItemType)
                .ThenInclude(itemType => itemType.ItemsClass)
                .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .Where(record => record.IdTypeCharacteristic == id)
                .ToListAsync();
        }
        public async Task Create(ItemsTypeCharacteristics record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
        }
        public void Delete(ItemsTypeCharacteristics record)
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
