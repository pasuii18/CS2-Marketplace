using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsTypeCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.Type
{
    public class ItemsTypeCharacteristicsService : IItemsTypeCharacteristicsService
    {
        private readonly IItemsTypeCharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public ItemsTypeCharacteristicsService(IItemsTypeCharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsTypeCharacteristicsReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ItemsTypeCharacteristicsReadDto>>(records);
        }
        public async Task Create(ItemsTypeCharacteristicsCreateDto createDto)
        {
            var model = _mapper.Map<ItemsTypeCharacteristics>(createDto);
            await _repo.Create(model);
        }
        public async Task Update(ItemsTypeCharacteristicsUpdateDto updateDto)
        {
            var record = await _repo.GetByIds(updateDto.IdItemType, updateDto.IdTypeCharacteristic);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemTypeCharacteristics with your ID is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(ItemsTypeCharacteristicsDeleteDto deleteDto)
        {
            var record = await _repo.GetByIds(deleteDto.IdItemType, deleteDto.IdTypeCharacteristic);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemTypeCharacteristics with your IDs is not found!");
            }
            
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
