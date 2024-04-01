using CSMarketApp.Domain.Core.Models.DealsModels;

namespace CSMarketApp.Domain.Core.Models.UsersModels
{
    public class Users
    {
        public int IdUser { get; set; }
        public int? IdUserProfilePicture { get; set; }
        public int IdRole { get; set; } = 1;
        public string? UUID { get; set; }
        public string? Username { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Description { get; set; } = "Hi! I'm using CSMarket!";

        public UsersPictures? UserProfilePicture { get; set; }
        public Roles Role { get; set; }
        public List<Deals>? Deals { get; set; }
        public List<DealOffers>? DealOffers { get; set; }
        public List<DealsHistory>? SellingHistory { get; set; }
        public List<DealsHistory>? BuyingHistory { get; set; }
    }
}