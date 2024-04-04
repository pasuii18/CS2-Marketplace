using AutoMapper;
using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;
using System.Data;
using System.Security.Authentication;
using System.Transactions;

namespace CSMarketApp.Infrastructure.Business.Services.DealsServices
{
    public class DealsService : IDealsService
    {
        private readonly IDealsRepo _dealsRepo;
        private readonly IDealOffersRepo _dealOffersRepo;
        private readonly IDealsHistoryRepo _dealsHistoryRepo;
        private readonly IUsersRepo _usersRepo;
        private readonly IMapper _mapper;

        public DealsService(IDealsRepo dealsRepo, IDealOffersRepo dealOffersRepo, IDealsHistoryRepo dealsHistoryRepo, IUsersRepo usersRepo, IMapper mapper)
        {
            _dealsRepo = dealsRepo;
            _dealOffersRepo = dealOffersRepo;
            _dealsHistoryRepo = dealsHistoryRepo;
            _usersRepo = usersRepo;
            _mapper = mapper;
        }
        
        public async Task<DealReadDto> GetDealById(int id)
        {
            var model = await _dealsRepo.GetRowById(id);
            if (model == null)
            {
                throw new KeyNotFoundException("Deal with your ID is not found!");
            }
            return _mapper.Map<DealReadDto>(model);
        }
        public async Task<IEnumerable<DealOffersReadDto>> GetDealOffers(int id)
        {
            var models = await _dealOffersRepo.GetAllByDealId(id);
            return _mapper.Map<IEnumerable<DealOffersReadDto>>(models);
        }
        public async Task DeleteDeal(int id, string uuid)
        {
            var deal = await _dealsRepo.GetRowById(id);
            if (deal == null)
            {
                throw new KeyNotFoundException("Deal with your ID is not found!");
            }
            if(deal.User.UUID != uuid)
            {
                throw new AuthenticationException("You cannot delete other people's deals!");
            }
            _dealsRepo.DeleteRow(deal);
            await _dealsRepo.SaveChanges();
        }
        public async Task CreateDeal(DealCreateDto dealCreateDto, string uuid)
        {
            var model = _mapper.Map<Deals>(dealCreateDto);
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null)
            {
                throw new AuthenticationException("User with your UUID is not found!");
            }
            model.IdUser = user.IdUser;
            await _dealsRepo.CreateRow(model);
            await _dealsRepo.SaveChanges();
        }
        public async Task AcceptDealOffer(int dealId, int offerId, string sellerUUID)
        {
            var seller = await _usersRepo.GetUserByUUID(sellerUUID) ?? throw new KeyNotFoundException("You are not existing!");
            var deal = await _dealsRepo.GetRowById(dealId) ?? throw new KeyNotFoundException("Deal with this ID is not found!");

            if (deal.IdUser != seller.IdUser)
            {
                throw new AuthenticationException("You cannot accept other's deals!");
            }

            var offer = await _dealOffersRepo.GetOfferById(offerId) ?? throw new KeyNotFoundException("DealOffer with this ID is not found!");

            if (offer.IdOfferer == deal.IdUser)
            {
                throw new DuplicateNameException("You cannot accept your own DealOffer!");
            }

            DealsHistory dealHistory = new DealsHistory
            {
                IdItem = deal.IdItem,
                IdSeller = deal.IdUser,
                IdBuyer = offer.IdOfferer,
                Price = offer.OfferPrice,
                Date = DateTime.Now
            };

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    _dealsRepo.DeleteRow(deal);
                    await _dealsRepo.SaveChanges();

                    await _dealsHistoryRepo.CreateRecord(dealHistory);
                    await _dealsHistoryRepo.SaveChanges();

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
