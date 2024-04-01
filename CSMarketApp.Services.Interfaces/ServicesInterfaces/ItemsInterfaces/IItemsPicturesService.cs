
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsPicturesService
    {
        Task<IEnumerable<ItemsPicturesReadDto>> GetAll();
        Task<byte[]> GetById(int id);
        Task<string> Delete(int id);
    }
}
