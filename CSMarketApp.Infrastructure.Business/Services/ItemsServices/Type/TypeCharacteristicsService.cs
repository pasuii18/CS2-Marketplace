using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.TypeCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using System.Data;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.Type
{
    public class TypeCharacteristicsService : ITemplateService<TypeCharacteristicsReadDto, TypeCharacteristicsCreateDto, TypeCharacteristicsUpdateDto>
    {
        private readonly ITemplateRepo<TypeCharacteristics> _repo;
        private readonly IMapper _mapper;

        public TypeCharacteristicsService(ITemplateRepo<TypeCharacteristics> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypeCharacteristicsReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<TypeCharacteristicsReadDto>>(records);
        }
        public async Task<TypeCharacteristicsReadDto> GetById(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("TypeCharacteristic with your ID is not found!");
            }
            return _mapper.Map<TypeCharacteristicsReadDto>(record);
        }
        public async Task<RecordIdReadDto> Create(TypeCharacteristicsCreateDto createDto)
        {
            var record = await _repo.GetByName(createDto.TypeCharacteristicName);
            if (record != null)
            {
                throw new DuplicateNameException("TypeCharacteristic with this name already exists!");
            }
            var model = _mapper.Map<TypeCharacteristics>(createDto);
            int id = await _repo.Create(model);
            return _mapper.Map<RecordIdReadDto>(id);
        }
        public async Task Update(TypeCharacteristicsUpdateDto updateDto)
        {
            var record = await _repo.GetById(updateDto.IdTypeCharacteristic);
            if (record == null)
            {
                throw new KeyNotFoundException("TypeCharacteristic with your ID is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("TypeCharacteristic with your ID is not found!");
            }
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
