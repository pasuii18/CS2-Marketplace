using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwt(UserProfileDto userReadDto);
        Task<string> Authentication(UserAuthDto userAuthDto);
        Task<string> Registration(UserCreateDto userCreateDto);
    }
}
