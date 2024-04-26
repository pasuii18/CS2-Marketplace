using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.UsersRepos
{
    public class UsersRepo : IUsersRepo
    {
        private readonly AppDbContext _context;

        public UsersRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(Users row)
        {
            if(row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            await _context.AddAsync(row);
        }
        public void DeleteUser(Users row)
        {
            if(row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            _context.Remove(row);
        }
        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _context.Users
                .Include(user => user.UserProfilePicture)
                .Include(user => user.Role)
                .Include(user => user.Deals)
                .Include(user => user.DealOffers)
                .Include(user => user.BuyingHistory)
                .Include(user => user.SellingHistory)
                .ToListAsync();
        }
        public async Task<Users?> GetUserByUUID(string uuid)
        {
            return await _context.Users
                .Include(user => user.UserProfilePicture)
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.UUID == uuid);    
        }
        public async Task<Users?> GetUserByLogin(string login)
        {
            return await _context.Users
                .Include(user => user.UserProfilePicture)
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Login == login);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
