using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos
{
    public class ItemsSkinsRepo : IItemsSkinsRepo
    {
        private readonly AppDbContext _context;

        public ItemsSkinsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Skins>> GetAllSkins()
        {
            return await _context.Skins
                .ToListAsync();
        }
        public async Task<Skins?> GetById(int id)
        {
            return await _context.Skins
                .FirstOrDefaultAsync(record => record.IdSkin == id);
        }
        public async Task<Skins?> GetByName(string name)
        {
            return await _context.Skins
                .FirstOrDefaultAsync(record => record.SkinName == name);
        }
        public async Task<int> Create(Skins record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdSkin;
        }
        public void Delete(Skins record)
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

        public async Task<int> GetOrCreate(Skins model)
        {
            var item = await _context.Skins
                .FirstOrDefaultAsync(record => record.SkinName == model.SkinName);

            if (item != null)
            {
                return item.IdSkin;
            }
            else
            {
                await Create(model);
                await SaveChanges();
                return model.IdSkin;
            }
        }
    }
}
