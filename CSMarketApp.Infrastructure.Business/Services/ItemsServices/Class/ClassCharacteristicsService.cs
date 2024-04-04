using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ClassCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using System.Data;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices.Class
{
    public class ClassCharacteristicsService : ITemplateService<ClassCharacteristicsReadDto, ClassCharacteristicsCreateDto, ClassCharacteristicsUpdateDto>
    {
        private readonly ITemplateRepo<ClassCharacteristics> _repo;
        private readonly IMapper _mapper;

        public ClassCharacteristicsService(ITemplateRepo<ClassCharacteristics> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassCharacteristicsReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ClassCharacteristicsReadDto>>(records);
        }
        public async Task<RecordIdReadDto> Create(ClassCharacteristicsCreateDto createDto)
        {
            var record = await _repo.GetByName(createDto.ClassCharacteristicName);
            if (record != null)
            {
                throw new DuplicateNameException("ClassCharacteristic with this name already exists!");
            }
            var model = _mapper.Map<ClassCharacteristics>(createDto);
            int id = await _repo.Create(model);
            return _mapper.Map<RecordIdReadDto>(id);
        }
        public async Task Update(ClassCharacteristicsUpdateDto updateDto)
        {
            var record = await _repo.GetById(updateDto.IdClassCharacteristic);
            if (record == null)
            {
                throw new KeyNotFoundException("ClassCharacteristic with your ID is not found!");
            }

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("ClassCharacteristic with your ID is not found!");
            }
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
