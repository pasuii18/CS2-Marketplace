using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.SubClass
{
    public class ItemsSubClassRepo : ITemplateRepo<ItemsSubClass>
    {
        private readonly AppDbContext _context;

        public ItemsSubClassRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemsSubClass>> GetAll()
        {
            return await _context.ItemsSubClass
                .ToListAsync();
        }
        public async Task<ItemsSubClass?> GetById(int id)
        {
            return await _context.ItemsSubClass
                .FirstOrDefaultAsync(record => record.IdItemSubClass == id);
        }
        public async Task<ItemsSubClass?> GetByName(string name)
        {
            return await _context.ItemsSubClass
                .FirstOrDefaultAsync(record => record.ItemSubClassName == name);
        }
        public async Task<int> Create(ItemsSubClass record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdItemSubClass;
        }
        public void Delete(ItemsSubClass record)
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

        public async Task<int> GetOrCreate(ItemsSubClass model)
        {
            var item = await _context.ItemsSubClass
                .FirstOrDefaultAsync(record => record.ItemSubClassName == model.ItemSubClassName);

            if(item != null)
            {
                return item.IdItemSubClass;
            }
            else
            {
                await Create(model);
                await SaveChanges();
                return model.IdItemSubClass;
            }
        }
    }
}
