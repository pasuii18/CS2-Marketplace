using AutoMapper;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Pictures;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.UsersServices
{
    public class UsersPicturesService : IUsersPicturesService
    {
        private readonly IUsersPicturesRepo _repo;
        private const string DefaultPicture = "images\\UsersProfilePictures\\default.jpg";


        public UsersPicturesService(IUsersPicturesRepo repo)
        {
            _repo = repo;
        }

        public async Task<byte[]> GetById(int id)
        {
            if(id == 0)
            {
                var defaultPicturePath = Path.Combine(Directory.GetCurrentDirectory(), DefaultPicture);
                var defaultPictureBytes = System.IO.File.ReadAllBytes(defaultPicturePath);
                return defaultPictureBytes;
            }

            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new FileNotFoundException("Record not found!");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), record.PicturePath);
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found!");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }
    }
}