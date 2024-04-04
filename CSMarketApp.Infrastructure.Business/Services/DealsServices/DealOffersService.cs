using System.Security.Authentication;
using AutoMapper;
using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.DealsServices
{
    public class DealOffersService : IDealOffersService
    {
        private readonly IDealOffersRepo _repo;
        private readonly IDealsRepo _dealsRepo;
        private readonly IUsersRepo _usersRepo;
        private readonly IMapper _mapper;

        public DealOffersService(IDealOffersRepo repo,IDealsRepo dealsRepo, IUsersRepo usersRepo, IMapper mapper)
        {
            _repo = repo;
            _dealsRepo = dealsRepo;
            _usersRepo = usersRepo;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<DealOffersReadDto>> GetAllByDealId(int id)
        {
            var record = await _repo.GetAllByDealId(id);
            if (record == null)
            {
                throw new KeyNotFoundException("DealOffers with your ID is not found!");
            }
            return _mapper.Map<IEnumerable<DealOffersReadDto>>(record);
        }
        public async Task<string> Create(DealOffersCreateDto createDto, string uuid)
        {
            var deal = await _dealsRepo.GetRowById(createDto.IdDeal);
            if (deal == null)
            {
                throw new NullReferenceException("Deal with this Id is not found!");
            }
            
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null)
            {
                throw new NullReferenceException("User with this UUID is not found!");
            }

            if (deal.IdUser == user.IdUser)
            {
                throw new Exception("You cant make an offer for your own deal!");
            }
            
            var model = _mapper.Map<DealOffers>(createDto);
            model.IdOfferer = user.IdUser;
            await _repo.Create(model);
            return "Offer was created successfully!";
        }
        public async Task Update(DealOffersUpdateDto updateDto, string uuid)
        {
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null) throw new KeyNotFoundException("User with your UUID is not found!");
            
            var record = await _repo.GetOfferById(updateDto.IdDealOffer);
            if (record == null) throw new KeyNotFoundException("DealOffer with your ID is not found!");

            if (user.IdUser != record.IdOfferer) throw new AuthenticationException("You can't update other's offers!");

            _mapper.Map(updateDto, record);
            await _repo.SaveChanges();
        }
        public async Task Delete(int id, string uuid)
        {
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null) throw new KeyNotFoundException("User with your UUID is not found!");
            
            var record = await _repo.GetOfferById(id);
            if (record == null) throw new KeyNotFoundException("DealOffer with your ID is not found!");
            
            if (user.IdUser != record.IdOfferer) throw new AuthenticationException("You can't update other's offers!");
            
            _repo.Delete(record);
            await _repo.SaveChanges();
        }
    }
}
