using CSMarketApp.Domain.Core.Models.UsersModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces
{
    public interface IUsersRepo
    {
        Task SaveChanges();
        Task<Users?> GetUserByUUID(string uuid);
        Task<Users?> GetUserByLogin(string login);
        Task<IEnumerable<Users>> GetAllUsers();
        Task CreateUser(Users row);
        void DeleteUser(Users row);
    }
}
