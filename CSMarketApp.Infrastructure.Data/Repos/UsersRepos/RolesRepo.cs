using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CSMarketApp.Infrastructure.Data.Repos.UsersRepos
{
    public class RolesRepo : IRolesRepo
    {
        private readonly AppDbContext _context;

        public RolesRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Roles>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Roles> GetRoleByName(string roleName)
        {
            return await _context.Roles
                .Where(roles => roles.RoleName == roleName)
                .FirstOrDefaultAsync();
        }
        public async Task<Roles> GetRoleById(int id)
        {
            return await _context.Roles
                .Where(roles => roles.IdRole == id)
                .FirstOrDefaultAsync();
        }
        public async Task CreateRole(Roles row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            await _context.AddAsync(row);
        }

        public void DeleteRole(Roles row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            _context.Remove(row);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
