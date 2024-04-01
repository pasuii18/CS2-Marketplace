using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Class
{
    public class ClassCharacteristicsRepo : ITemplateRepo<ClassCharacteristics>
    {
        private readonly AppDbContext _context;

        public ClassCharacteristicsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ClassCharacteristics>> GetAll()
        {
            return await _context.ClassCharacteristics
                .ToListAsync();
        }
        public async Task<ClassCharacteristics?> GetById(int id)
        {
            return await _context.ClassCharacteristics
                .FirstOrDefaultAsync(record => record.IdClassCharacteristic == id);
        }
        public async Task<ClassCharacteristics?> GetByName(string name)
        {
            return await _context.ClassCharacteristics
                .FirstOrDefaultAsync(record => record.ClassCharacteristicName == name);
        }
        public async Task<int> Create(ClassCharacteristics record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdClassCharacteristic;
        }
        public void Delete(ClassCharacteristics record)
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

        public async Task<int> GetOrCreate(ClassCharacteristics model)
        {
            var item = await _context.ClassCharacteristics
                .FirstOrDefaultAsync(record => record.ClassCharacteristicName == model.ClassCharacteristicName);

            if (item != null)
            {
                return item.IdClassCharacteristic;
            }
            else
            {
                await Create(model);
                await SaveChanges();
                return model.IdClassCharacteristic;
            }
        }
    }
}
