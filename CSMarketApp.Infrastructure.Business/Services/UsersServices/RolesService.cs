using AutoMapper;
using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.UsersServices
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepo _repo;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RolesReadDto>> GetAllRoles()
        {
            var roles = await _repo.GetAllRoles();
            return _mapper.Map<IEnumerable<RolesReadDto>>(roles);
        }

        public async Task CreateRole(RolesCreateDto row)
        {
            var role = await _repo.GetRoleByName(row.RoleName);
            if (role != null)
            {
                throw new KeyNotFoundException("Role with this name already exists!");
            }
            var roleModel = _mapper.Map<Roles>(row);
            await _repo.CreateRole(roleModel);
            await _repo.SaveChanges();
        }

        public async Task UpdateRole(RolesUpdateDto row)
        {
            var role = await _repo.GetRoleById(row.IdRole);
            if (role == null)
            {
                throw new KeyNotFoundException("Role with your ID is not found!");
            }
            _mapper.Map(row, role);
            await _repo.SaveChanges();
        }

        public async Task DeleteRole(string roleName)
        {
            var role = await _repo.GetRoleByName(roleName);
            if (role == null)
            {
                throw new KeyNotFoundException("Role with your name is not found!");
            }
            _repo.DeleteRole(role);
            await _repo.SaveChanges();
        }
    }
}
