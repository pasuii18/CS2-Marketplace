using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsType;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.Type
{
    public class ItemsTypeService : ITemplateService<ItemsTypeReadDto, ItemsTypeCreateDto, ItemsTypeUpdateDto>
    {
        private readonly ITemplateRepo<ItemsType> _repo;
        private readonly IMapper _mapper;

        public ItemsTypeService(ITemplateRepo<ItemsType> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsTypeReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ItemsTypeReadDto>>(records);
        }
        public async Task<RecordIdReadDto> Create(ItemsTypeCreateDto createDto)
        {
            var model = _mapper.Map<ItemsType>(createDto);
            int id = await _repo.Create(model);
            return _mapper.Map<RecordIdReadDto>(id);
        }
        public async Task Update(ItemsTypeUpdateDto updateDto)
        {
            var record = await _repo.GetById(updateDto.IdItemType);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemType with your ID is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemType with your ID is not found!");
            }
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
