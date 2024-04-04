using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClass;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.Class
{
    public class ItemsClassService : ITemplateService<ItemsClassReadDto, ItemsClassCreateDto, ItemsClassUpdateDto>
    {
        private readonly ITemplateRepo<ItemsClass> _repo;
        private readonly IMapper _mapper;

        public ItemsClassService(ITemplateRepo<ItemsClass> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsClassReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ItemsClassReadDto>>(records);
        }
        public async Task<RecordIdReadDto> Create(ItemsClassCreateDto createDto)
        {
            var model = _mapper.Map<ItemsClass>(createDto);
            int id = await _repo.Create(model);
            return _mapper.Map<RecordIdReadDto>(id);
        }
        public async Task Update(ItemsClassUpdateDto updateDto)
        {
            var record = await _repo.GetById(updateDto.IdItemClass);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemClass with your ID is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemClass with your ID is not found!");
            }
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}