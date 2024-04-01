using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.UsersRepos
{
    public class UsersPicturesRepo : IUsersPicturesRepo
    {
        private readonly AppDbContext _context;

        public UsersPicturesRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsersPictures>> GetAll()
        {
            return await _context.UsersPictures.ToListAsync();
        }
        public async Task<UsersPictures?> GetById(int id)
        {
            return await _context.UsersPictures.FirstOrDefaultAsync(item => item.IdUserProfilePicture == id);
        }
        public async Task<int> Create(UsersPictures record)
        {
            if (record == null)
            {
                throw new ArgumentNullException(nameof(record));
            }

            await _context.AddAsync(record);
            await SaveChanges();
            return record.IdUserProfilePicture;
        }
        public void Delete(UsersPictures record)
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
