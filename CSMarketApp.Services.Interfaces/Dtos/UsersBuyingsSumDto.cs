using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;

namespace CSMarketApp.Services.Interfaces.Dtos
{
    public class UsersBuyingsSumDto
    {
        public UserProfileDto User { get; set; }
        public decimal Sum { get; set; }
    }
}