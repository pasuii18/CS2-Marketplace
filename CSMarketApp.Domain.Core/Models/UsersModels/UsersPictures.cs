namespace CSMarketApp.Domain.Core.Models.UsersModels
{
    public class UsersPictures
    {
        public int IdUserProfilePicture { get; set; }
        public string? PicturePath { get; set; }
        public Users User { get; set; }
    }
}
