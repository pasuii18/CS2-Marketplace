using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Type
{
    public class TypeCharacteristicsRepo : ITemplateRepo<TypeCharacteristics>
    {
        private readonly AppDbContext _context;

        public TypeCharacteristicsRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TypeCharacteristics>> GetAll()
        {
            return await _context.TypeCharacteristics
                .ToListAsync();
        }
        public async Task<TypeCharacteristics?> GetById(int id)
        {
            return await _context.TypeCharacteristics
                .FirstOrDefaultAsync(record => record.IdTypeCharacteristic == id);
        }
        public async Task<TypeCharacteristics?> GetByName(string name)
        {
            return await _context.TypeCharacteristics
                .FirstOrDefaultAsync(record => record.TypeCharacteristicName == name);
        }
        public async Task<int> Create(TypeCharacteristics record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdTypeCharacteristic;
        }
        public void Delete(TypeCharacteristics record)
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

        public async Task<int> GetOrCreate(TypeCharacteristics model)
        {
            var item = await _context.TypeCharacteristics
                .FirstOrDefaultAsync(record => record.TypeCharacteristicName == model.TypeCharacteristicName);

            if (item != null)
            {
                return item.IdTypeCharacteristic;
            }
            else
            {
                await Create(model);
                await SaveChanges();
                return model.IdTypeCharacteristic;
            }
        }
    }
}
