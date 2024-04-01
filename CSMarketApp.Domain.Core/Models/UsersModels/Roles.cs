namespace CSMarketApp.Domain.Core.Models.UsersModels
{
    public class Roles
    {
        public int IdRole { get; set; }
        public string? RoleName { get; set; }
        public List<Users> Users { get; set; }
    }
}
