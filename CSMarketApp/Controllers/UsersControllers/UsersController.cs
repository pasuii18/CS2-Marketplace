using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;

namespace CSMarketApp.Controllers.UsersControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = "Developer")]
        public async Task<ActionResult<IEnumerable<UserDebugDto>>> GetAllUsers()
        {
            return Ok(await _usersService.GetAllUsers());
        }
        [HttpGet("{uuid}", Name = "GetUserByUUID")]
        public async Task<ActionResult<UserProfileDto>> GetUserByUUID(string uuid)
        { 
            return Ok(await _usersService.GetUserByUUID(uuid));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromForm]UserUpdateDto userUpdateDto)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                throw new KeyNotFoundException("UUID not found in token claims.");
            }
            await _usersService.UpdateUser(userUpdateDto, uuid);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(UserAuthDto userAuthDto)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                throw new KeyNotFoundException("UUID not found in token claims.");
            }
            await _usersService.DeleteUser(userAuthDto, uuid);
            return NoContent();
        }
        [HttpPut("set-role")]
        [Authorize(Roles = "Developer")]
        public async Task<ActionResult> UpdateUserRole(UserRoleUpdateDto userRoleUpdateDto)
        {
            string uuid = User.Claims.FirstOrDefault(c => c.Type == "uuid")?.Value;
            if (uuid == null)
            {
                throw new KeyNotFoundException("UUID not found in token claims.");
            }
            if (uuid == userRoleUpdateDto.UUID)
            {
                throw new DuplicateNameException("You cannot change yourself role. =)");
            }
            await _usersService.UpdateUserRole(userRoleUpdateDto);
            return Ok();
        }

        [HttpGet("{uuid}/deals")]
        public async Task<ActionResult<IEnumerable<DealReadDto>>> GetUserDeals(string uuid)
        {
            return Ok(await _usersService.GetUserDeals(uuid));
        }
        [HttpGet("{uuid}/offers")]
        public async Task<ActionResult<IEnumerable<DealOffersReadDto>>> GetUserOffers(string uuid)
        {
            return Ok(await _usersService.GetUserOffers(uuid));
        }
        [HttpGet("{uuid}/sellings")]
        public async Task<ActionResult<IEnumerable<UserDealsHistoryReadDto>>> GetAllUserSellingHistory(string uuid)
        {
            return Ok(await _usersService.GetAllUserSellingHistory(uuid));
        }
        [HttpGet("{uuid}/buyings")]
        public async Task<ActionResult<IEnumerable<UserDealsHistoryReadDto>>> GetAllUserBuyingHistory(string uuid)
        {
            return Ok(await _usersService.GetAllUserBuyingHistory(uuid));
        }
    }
}
