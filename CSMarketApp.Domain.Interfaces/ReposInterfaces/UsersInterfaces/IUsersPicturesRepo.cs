using CSMarketApp.Domain.Core.Models.UsersModels;

namespace CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces
{
    public interface IUsersPicturesRepo
    {
        Task<IEnumerable<UsersPictures>> GetAll();
        Task<UsersPictures?> GetById(int id);
        Task<int> Create(UsersPictures record);
        void Delete(UsersPictures record);
        Task SaveChanges();
    }
}
