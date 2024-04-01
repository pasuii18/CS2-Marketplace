using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Class
{
    public class ItemsClassRepo : ITemplateRepo<ItemsClass>
    {
        private readonly AppDbContext _context;

        public ItemsClassRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemsClass>> GetAll()
        {
            return await _context.ItemsClass
                .Include(itemClass => itemClass.ItemsSubClass)
                .ToListAsync();
        }
        public async Task<ItemsClass?> GetById(int id)
        {
            return await _context.ItemsClass
                .Include(itemClass => itemClass.ItemsSubClass)
                .FirstOrDefaultAsync(record => record.IdItemClass == id);
        }
        public async Task<ItemsClass?> GetByName(string name)
        {
            return await _context.ItemsClass
                .Include(itemClass => itemClass.ItemsSubClass)
                .FirstOrDefaultAsync(record => record.ItemClassName == name);
        }
        public async Task<int> Create(ItemsClass record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdItemClass;
        }
        public void Delete(ItemsClass record)
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

        public async Task<int> GetOrCreate(ItemsClass model)
        {
            var item = await _context.ItemsClass
                .FirstOrDefaultAsync(record => record.IdItemSubClass == model.IdItemSubClass);

            if (item != null)
            {
                return item.IdItemClass;
            }
            else
            {
                await Create(model);
                await SaveChanges();
                return model.IdItemClass;
            }
        }
    }
}
