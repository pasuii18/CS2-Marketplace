using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSMarketApp.Controllers.UsersControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Developer")]
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesReadDto>>> GetAllRoles()
        {
            return Ok(await _rolesService.GetAllRoles());
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(RolesCreateDto rolesCreateDto)
        {
            await _rolesService.CreateRole(rolesCreateDto);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRole(RolesUpdateDto rolesUpdateDto)
        {
            await _rolesService.UpdateRole(rolesUpdateDto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRole(string roleName)
        {
            await _rolesService.DeleteRole(roleName);
            return NoContent();
        }
    }
}
