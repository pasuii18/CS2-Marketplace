
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces
{
    public interface IRolesService
    {
        Task<IEnumerable<RolesReadDto>> GetAllRoles();
        Task CreateRole(RolesCreateDto row);
        Task UpdateRole(RolesUpdateDto row);
        Task DeleteRole(string roleName);
    }
}
