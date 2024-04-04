
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Pictures;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces
{
    public interface IUsersPicturesService
    {
        Task<byte[]> GetById(int id);
    }
}
