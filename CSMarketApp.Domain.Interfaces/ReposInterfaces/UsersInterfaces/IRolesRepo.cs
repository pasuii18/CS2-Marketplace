using CSMarketApp.Domain.Core.Models.UsersModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces
{
    public interface IRolesRepo
    {
        Task<IEnumerable<Roles>> GetAllRoles();
        Task<Roles> GetRoleByName(string roleName);
        Task<Roles> GetRoleById(int id);
        Task CreateRole(Roles row);
        void DeleteRole(Roles row);
        Task SaveChanges();
    }
}
