using AutoMapper;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.DealsServices
{
    public class DealsHistoryService : IDealsHistoryService
    {
        private readonly IDealsHistoryRepo _dealsHistoryRepo;
        private readonly IMapper _mapper;
        public DealsHistoryService(IDealsHistoryRepo dealsHistoryRepo, IMapper mapper)
        {
            _dealsHistoryRepo = dealsHistoryRepo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DealHistoryReadDto>> GetAllHistory()
        {
            var models = await _dealsHistoryRepo.GetAllHistory() ?? throw new KeyNotFoundException("There is still no history! May be you should make some? =)");
            return _mapper.Map<IEnumerable<DealHistoryReadDto>>(models);
        }
    }
}
