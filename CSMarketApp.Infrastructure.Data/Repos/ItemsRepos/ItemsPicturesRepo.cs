using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos
{
    public class ItemsPicturesRepo : IItemsPicturesRepo
    {
        private readonly AppDbContext _context;

        public ItemsPicturesRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemsPictures>> GetAll()
        {
            return await _context.ItemsPictures.ToListAsync();
        }
        public async Task<ItemsPictures?> GetById(int id)
        {
            return await _context.ItemsPictures.FirstOrDefaultAsync(item => item.IdItemPicture == id);
        }
        public async Task<int> Create(ItemsPictures record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }
            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdItemPicture;
        }
        public void Delete(ItemsPictures record)
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
