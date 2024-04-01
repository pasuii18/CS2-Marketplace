
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Pictures;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces
{
    public interface IUsersPicturesService
    {
        Task<IEnumerable<UsersPicturesReadDto>> GetAll();
        Task<byte[]> GetById(int id);
        Task<string> Delete(int id);
    }
}
