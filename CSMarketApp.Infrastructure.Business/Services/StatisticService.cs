using AutoMapper;
using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Infrastructure.Data;
using CSMarketApp.Infrastructure.Data.Repos.ItemsRepos;
using CSMarketApp.Infrastructure.Data.Repos.UsersRepos;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMarketApp.Infrastructure.Business.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUsersRepo _usersRepo;
        private readonly IItemsRepo _itemsRepo;
        private readonly IMapper _mapper;

        public StatisticService(IUsersRepo usersRepo, IItemsRepo itemsRepo, IMapper mapper)
        {
            _usersRepo = usersRepo;
            _itemsRepo = itemsRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsersBuyingsSumDto>> GetTopOfUsersByBuyings()
        {
            var users = await _usersRepo.GetAllUsers();

            var topUsers = users.GroupBy(u => u.IdUser)
                .Select(g => new
                {
                    User = g.First(),
                    TotalPurchaseSum = g.Sum(u => u.BuyingHistory.Sum(dh => dh.Price)) // SellingHistory
                })
                .OrderByDescending(u => u.TotalPurchaseSum)
                .Take(5)
                .ToList();

            var list = new List<UsersBuyingsSumDto>();
            foreach (var user in topUsers) 
            {
                var record = new UsersBuyingsSumDto
                {
                    User = _mapper.Map<UserProfileDto>(user.User),
                    Sum = user.TotalPurchaseSum
                };
                list.Add(record);
            }

            return list;
        }

        public async Task<ItemSellingsDto> GetTopSellingItem()
        {
            var items = await _itemsRepo.GetAllItems();

            var topItem = items
                .Select(item => new
                {
                    Item = item,
                    DealsCount = item.Deals.Count(),
                    OffersCount = item.Deals.SelectMany(deal => deal.DealOffers).Count()
                })
                .OrderByDescending(x => x.DealsCount + x.OffersCount)
                .FirstOrDefault();

            var dto = new ItemSellingsDto
            {
                Item = _mapper.Map<ItemsReadDto>(topItem.Item),
                DealsCount = topItem.DealsCount,
                OffersCount = topItem.OffersCount
            };

            return dto;
        }

        public async Task<IEnumerable<UserStatDto>> GetTopWorstUsers()
        {
            var users = await _usersRepo.GetAllUsers();

            var topUsers = users
                .Select(user => new
                {
                    User = user,
                    TotalDeals = user.Deals.Count(),
                    TotalDealOffers = user.DealOffers.Count(),
                    TotalTransactions = user.BuyingHistory
                        .Where(deal => deal.IdBuyer == user.IdUser || deal.IdSeller == user.IdUser)
                        .Count()
                        +
                        user.SellingHistory
                        .Where(deal => deal.IdBuyer == user.IdUser || deal.IdSeller == user.IdUser)
                        .Count()
                })
                .Where(user => user.TotalDeals > 0 || user.TotalDealOffers > 0 || user.TotalTransactions > 0)
                .OrderByDescending(x => x.TotalDeals + x.TotalDealOffers)
                .ThenBy(x => x.TotalTransactions)
                .Take(5)
                .ToList();


            var list = new List<UserStatDto>();
            foreach (var user in topUsers)
            {
                var record = new UserStatDto
                {
                    User = _mapper.Map<UserProfileDto>(user.User),
                    TotalDeals = user.TotalDeals,
                    TotalDealOffers = user.TotalDealOffers,
                    TotalTransactions = user.TotalTransactions
                };
                list.Add(record);
            }

            return list;
        }
    }
}
