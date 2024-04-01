using AutoMapper;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Pictures;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;

namespace CSMarketApp.Infrastructure.Business.Services.UsersServices
{
    public class UsersPicturesService : IUsersPicturesService
    {
        private readonly IUsersPicturesRepo _repo;
        private readonly IMapper _mapper;
        private const string DefaultPicture = "images\\UsersProfilePictures\\default.jpg";


        public UsersPicturesService(IUsersPicturesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UsersPicturesReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<UsersPicturesReadDto>>(records);
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
        public async Task<string> Delete(int id)
        {
            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new KeyNotFoundException("Picture with your ID is not found!");
            }
            _repo.Delete(record);
            await _repo.SaveChanges();
            return "Profile picture was deleted succesfully!";
        }
    }
}