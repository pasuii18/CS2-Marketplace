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
        private const string DefaultItemPicture = "images\\ItemsPictures\\default.jpg";

        public ItemsPicturesService(IItemsPicturesRepo repo)
        {
            _repo = repo;
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
    }
}