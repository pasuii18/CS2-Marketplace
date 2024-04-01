
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces
{
    public interface IItemsService
    {
        Task<IEnumerable<ItemsReadDto>> GetAllItems();
        Task<ItemDetailedReadDto> GetItemById(int id);
        Task<IEnumerable<ItemDealsReadDto>> GetItemDeals(int id);
        Task<RecordIdReadDto> CreateItem(ItemCreateSpecialDto itemCreateDto);
        Task DeleteItem(int id);
        Task<IEnumerable<DealHistoryReadDto>> GetAllItemHistory(int idItem);
    }
}
