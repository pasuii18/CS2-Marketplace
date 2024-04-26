using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSMarketApp.Services.Interfaces.Dtos;

namespace CSMarketApp.Services.Interfaces.ServicesInterfaces
{
    public interface IStatisticService
    {
        public Task<IEnumerable<UsersBuyingsSumDto>> GetTopOfUsersByBuyings();
        public Task<ItemSellingsDto> GetTopSellingItem();
        public Task<IEnumerable<UserStatDto>> GetTopWorstUsers();
    }
}
