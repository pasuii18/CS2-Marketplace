using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDebugDto>> GetAllUsers();
        Task<UserProfileDto> GetUserByUUID(string uuid);
        Task<IEnumerable<DealReadDto>> GetUserDeals(string uuid);
        Task<IEnumerable<DealOffersReadDto>> GetUserOffers(string uuid);
        Task<IEnumerable<UserDealsHistoryReadDto>> GetAllUserSellingHistory(string sellerUUID);
        Task<IEnumerable<UserDealsHistoryReadDto>> GetAllUserBuyingHistory(string buyerUUID);
        Task UpdateUser(UserUpdateDto userUpdateDto, string uuid);
        Task DeleteUser(UserAuthDto userAuthDto, string uuid);
        Task UpdateUserRole(UserRoleUpdateDto userRoleUpdateDto);
    }
}
