using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMarketApp.Services.Interfaces.Dtos
{
    public class UserStatDto
    {
        public UserProfileDto User { get; set; }
        public int TotalDeals { get; set; }
        public int TotalDealOffers { get; set; }
        public int TotalTransactions { get; set; }
    }
}
