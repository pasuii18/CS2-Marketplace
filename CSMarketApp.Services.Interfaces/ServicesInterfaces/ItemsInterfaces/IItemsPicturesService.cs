
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsPicturesService
    {
        Task<byte[]> GetById(int id);
    }
}
