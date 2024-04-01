using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClassCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.Class
{
    public class ItemsClassCharacteristicsService : IItemsClassCharacteristicsService
    {
        private readonly IItemsClassCharacteristicsRepo _repo;
        private readonly IMapper _mapper;

        public ItemsClassCharacteristicsService(IItemsClassCharacteristicsRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ItemsClassCharacteristicsReadDto>>(records);
        }
        public async Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetByItemId(int id)
        {
            var record = await _repo.GetByItemId(id);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemClassCharacteristics with your ItemId is not found!");
            }
            return _mapper.Map<IEnumerable<ItemsClassCharacteristicsReadDto>>(record);
        }
        public async Task<IEnumerable<ItemsClassCharacteristicsReadDto>> GetByCharacteristicId(int id)
        {
            var record = await _repo.GetByCharacteristicId(id);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemClassCharacteristics with your CharacteristicId is not found!");
            }
            return _mapper.Map<IEnumerable<ItemsClassCharacteristicsReadDto>>(record);
        }
        public async Task Create(ItemsClassCharacteristicsCreateDto createDto)
        {
            var model = _mapper.Map<ItemsClassCharacteristics>(createDto);
            await _repo.Create(model);
        }
        public async Task Update(ItemsClassCharacteristicsUpdateDto updateDto)
        {
            var record = await _repo.GetByIds(updateDto.IdItemClass, updateDto.IdClassCharacteristic);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemClassCharacteristics with your IDs is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(ItemsClassCharacteristicsDeleteDto deleteDto)
        {
            var record = await _repo.GetByIds(deleteDto.IdItemClass, deleteDto.IdClassCharacteristic);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemClassCharacteristics with your IDs is not found!");
            }

            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
