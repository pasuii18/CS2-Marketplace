using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Type
{
    public class ItemsTypeRepo : ITemplateRepo<ItemsType>
    {
        private readonly AppDbContext _context;

        public ItemsTypeRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ItemsType>> GetAll()
        {
            return await _context.ItemsType
                .Include(item => item.ItemsClass)
                .ThenInclude(item => item.ItemsSubClass)
                .ToListAsync();
        }
        public async Task<ItemsType?> GetById(int id)
        {
            return await _context.ItemsType
                .Include(item => item.ItemsClass)
                .ThenInclude(item => item.ItemsSubClass)
                .FirstOrDefaultAsync(record => record.IdItemType == id);
        }
        public async Task<ItemsType?> GetByName(string name)
        {
            return await _context.ItemsType
                .Include(item => item.ItemsClass)
                .ThenInclude(item => item.ItemsSubClass)
                .FirstOrDefaultAsync(record => record.ItemTypeName == name);
        }
        public async Task<int> Create(ItemsType record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdItemType;
        }
        public void Delete(ItemsType record)
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

        public async Task<int> GetOrCreate(ItemsType model)
        {
            var item = await _context.ItemsType
                .FirstOrDefaultAsync(record => record.IdItemClass == model.IdItemClass);

            if (item != null)
            {
                return item.IdItemType;
            }
            else
            {
                await Create(model);
                await SaveChanges();
                return model.IdItemType;
            }
        }
    }
}
