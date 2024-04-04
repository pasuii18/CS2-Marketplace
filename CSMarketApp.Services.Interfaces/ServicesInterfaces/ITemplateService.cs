
using CSMarketApp.Services.Interfaces.Dtos;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces
{
    public interface ITemplateService<ReadT, CreateT, UpdateT>
    {
        Task<IEnumerable<ReadT>> GetAll();
        Task<RecordIdReadDto> Create(CreateT createDto);
        Task Update(UpdateT updateDto);
        Task Delete(int id);
    }
}
