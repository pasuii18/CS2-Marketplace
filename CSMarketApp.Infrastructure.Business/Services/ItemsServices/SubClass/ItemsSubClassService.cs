using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.SubClass.ItemsSubClass;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using System.Data;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.SubClass
{
    public class ItemsSubClassService : ITemplateService<ItemsSubClassReadDto, ItemsSubClassCreateDto, ItemsSubClassUpdateDto>
    {
        private readonly ITemplateRepo<ItemsSubClass> _repo;
        private readonly IMapper _mapper;

        public ItemsSubClassService(ITemplateRepo<ItemsSubClass> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsSubClassReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ItemsSubClassReadDto>>(records);
        }
        public async Task<RecordIdReadDto> Create(ItemsSubClassCreateDto createDto)
        {
            var record = await _repo.GetByName(createDto.ItemSubClassName);
            if (record != null)
            {
                throw new DuplicateNameException("ItemSubClass with this name already exists!");
            }
            var model = _mapper.Map<ItemsSubClass>(createDto);
            int id = await _repo.Create(model);
            return _mapper.Map<RecordIdReadDto>(id);
        }
        public async Task Update(ItemsSubClassUpdateDto updateDto)
        {
            var record = await _repo.GetById(updateDto.IdItemSubClass);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemSubClass with your ID is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("ItemSubClass with your ID is not found!");
            }
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
