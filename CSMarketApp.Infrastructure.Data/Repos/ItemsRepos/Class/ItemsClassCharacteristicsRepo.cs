using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Class
{
    public class ItemsClassCharacteristicsRepo : IItemsClassCharacteristicsRepo
    {
        private readonly AppDbContext _context;

        public ItemsClassCharacteristicsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemsClassCharacteristics>> GetAll()
        {
            return await _context.ItemsClassCharacteristics
                .Include(itemClassCharacteristics => itemClassCharacteristics.ClassCharacteristic)
                .Include(itemClassCharacteristics => itemClassCharacteristics.ItemClass)
                    .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .ToListAsync();
        }

        public async Task<ItemsClassCharacteristics> GetByIds(int idItem, int idCharacteristic)
        {
            return await _context.ItemsClassCharacteristics
                .Include(itemClassCharacteristics => itemClassCharacteristics.ClassCharacteristic)
                .Include(itemClassCharacteristics => itemClassCharacteristics.ItemClass)
                .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .Where(record => record.IdItemClass == idItem && record.IdClassCharacteristic == idCharacteristic)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ItemsClassCharacteristics>> GetByItemId(int id)
        {
            return await _context.ItemsClassCharacteristics
                .Include(itemClassCharacteristics => itemClassCharacteristics.ClassCharacteristic)
                .Include(itemClassCharacteristics => itemClassCharacteristics.ItemClass)
                    .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .Where(record => record.IdItemClass == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<ItemsClassCharacteristics>> GetByCharacteristicId(int id)
        {
            return await _context.ItemsClassCharacteristics
                .Include(itemClassCharacteristics => itemClassCharacteristics.ClassCharacteristic)
                .Include(itemClassCharacteristics => itemClassCharacteristics.ItemClass)
                .ThenInclude(itemClass => itemClass.ItemsSubClass)
                .Where(record => record.IdClassCharacteristic == id)
                .ToListAsync();
        }
        public async Task Create(ItemsClassCharacteristics record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
        }
        public void Delete(ItemsClassCharacteristics record)
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
