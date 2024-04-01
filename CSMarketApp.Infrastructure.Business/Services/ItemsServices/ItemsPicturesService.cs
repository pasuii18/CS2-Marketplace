using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Infrastructure.Business.Algorithms;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Http;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices
{
    public class ItemsPicturesService : IItemsPicturesService
    {
        private readonly IItemsPicturesRepo _repo;
        private readonly IMapper _mapper;
        private const string DefaultItemPicture = "images\\ItemsPictures\\default.jpg";

        public ItemsPicturesService(IItemsPicturesRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemsPicturesReadDto>> GetAll()
        {
            var records = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ItemsPicturesReadDto>>(records);
        }
        public async Task<byte[]> GetById(int id)
        {
            if (id == 0)
            {
                var DefaultItemPicturePath = Path.Combine(Directory.GetCurrentDirectory(), DefaultItemPicture);
                var DefaultItemPictureBytes = System.IO.File.ReadAllBytes(DefaultItemPicturePath);
                return DefaultItemPictureBytes;
            }

            var record = await _repo.GetById(id);
            if (record == null)
            {
                throw new FileNotFoundException("Record not found!");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), record.ItemPicturePath);
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