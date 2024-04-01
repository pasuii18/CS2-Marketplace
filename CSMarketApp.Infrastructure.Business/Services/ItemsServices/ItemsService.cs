using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Infrastructure.Business.Algorithms;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using Microsoft.AspNetCore.Http;

namespace CSMarketApp.Infrastructure.Business.Services.ItemsServices
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepo _itemsRepo;
        private readonly IItemsSkinsRepo _itemsSkinsRepo;
        private readonly IDealsRepo _dealsRepo;
        private readonly IDealsHistoryRepo _dealsHistoryRepo;
        private readonly ITemplateRepo<ItemsSubClass> _itemsSubClassRepo;
        private readonly ITemplateRepo<ItemsClass> _itemsClassRepo;
        private readonly ITemplateRepo<ItemsType> _itemsTypeRepo;
        private readonly ITemplateRepo<ClassCharacteristics> _classCharacteristicsRepo;
        private readonly ITemplateRepo<TypeCharacteristics> _typeCharacteristicsRepo;
        private readonly IItemsClassCharacteristicsRepo _itemsClassCharacteristicsRepo;
        private readonly IItemsTypeCharacteristicsRepo _itemsTypeCharacteristicsRepo;
        private readonly IMapper _mapper;

        private readonly IItemsPicturesRepo _itemsPicturesRepo;
        private const string ItemsPicturesFolder = "images\\ItemsPictures";

        public ItemsService
            (
            IItemsRepo itemsRepo,
            IDealsRepo dealsRepo,
            IDealsHistoryRepo dealsHistoryRepo,
            ITemplateRepo<ItemsSubClass> itemsSubClassRepo,
            ITemplateRepo<ItemsClass> itemsClassRepo,
            ITemplateRepo<ItemsType> itemsTypeRepo,
            ITemplateRepo<ClassCharacteristics> classCharacteristicsRepo,
            ITemplateRepo<TypeCharacteristics> typeCharacteristicsRepo,
            IItemsClassCharacteristicsRepo itemsClassCharacteristicsRepo,
            IItemsTypeCharacteristicsRepo itemsTypeCharacteristicsRepo,
            IMapper mapper,
            IItemsPicturesRepo itemsPicturesRepo,
            IItemsSkinsRepo itemsSkinsRepo
            )
        {
            _itemsRepo = itemsRepo;
            _dealsRepo = dealsRepo;
            _dealsHistoryRepo = dealsHistoryRepo;
            _itemsSubClassRepo = itemsSubClassRepo;
            _itemsClassRepo = itemsClassRepo;
            _itemsTypeRepo = itemsTypeRepo;
            _classCharacteristicsRepo = classCharacteristicsRepo;
            _typeCharacteristicsRepo = typeCharacteristicsRepo;
            _itemsClassCharacteristicsRepo = itemsClassCharacteristicsRepo;
            _itemsTypeCharacteristicsRepo = itemsTypeCharacteristicsRepo;
            _mapper = mapper;
            _itemsPicturesRepo = itemsPicturesRepo;
            _itemsSkinsRepo = itemsSkinsRepo;
        }

        public async Task<IEnumerable<ItemsReadDto>> GetAllItems()
        {
            var items = await _itemsRepo.GetAllItems();
            return _mapper.Map<IEnumerable<ItemsReadDto>>(items);
        }
        public async Task<ItemDetailedReadDto> GetItemById(int id)
        {
            var item = await _itemsRepo.GetItemById(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item with your ID is not found!");
            }
            return _mapper.Map<ItemDetailedReadDto>(item);
        }
        public async Task<IEnumerable<ItemDealsReadDto>> GetItemDeals(int id)
        {
            var itemDeals = await _dealsRepo.GetItemRows(id);
            return _mapper.Map<IEnumerable<ItemDealsReadDto>>(itemDeals);
        }

        public async Task<RecordIdReadDto> CreateItem(ItemCreateSpecialDto itemCreateDto)
        {
            if (itemCreateDto.Rarity < 0 || itemCreateDto.Rarity > 6)
            {
                throw new InvalidDataException("Rarity with your ID is not found!");
            }

            var subClassModel = _mapper.Map<ItemsSubClass>(itemCreateDto.ItemSubClassName);
            int idSubClass = await _itemsSubClassRepo.GetOrCreate(subClassModel);
            
            ItemsClass classModel = new ItemsClass() { IdItemSubClass = idSubClass, ItemClassName = itemCreateDto.ItemClassName };
            int idClass = await _itemsClassRepo.GetOrCreate(classModel);

            ItemsType typeModel = new ItemsType() { IdItemClass = idClass, ItemTypeName = itemCreateDto.ItemTypeName };
            int idType = await _itemsTypeRepo.GetOrCreate(typeModel);

            var classCharacteristicsItemValueModels = await _itemsClassCharacteristicsRepo.GetByItemId(idClass);
            foreach (var characteristic in itemCreateDto.ItemClassCharacteristics)
            {
                var classCharacteristicsModel = _mapper.Map<ClassCharacteristics>(characteristic.ClassCharacteristicName);
                var idClassCharacteristic = await _classCharacteristicsRepo.GetOrCreate(classCharacteristicsModel);

                var exists = classCharacteristicsItemValueModels?.Any(model => model.IdClassCharacteristic == idClassCharacteristic) ?? false;
                if (!exists)
                {
                    ItemsClassCharacteristics itemsClassCharacteristics = new ItemsClassCharacteristics
                    {
                        IdItemClass = idClass,
                        IdClassCharacteristic = idClassCharacteristic,
                        ClassCharacteristicValue = characteristic.ClassCharacteristicValue
                    };
                    await _itemsClassCharacteristicsRepo.Create(itemsClassCharacteristics);
                }
            }

            var typeCharacteristicsItemValueModels = await _itemsTypeCharacteristicsRepo.GetByItemId(idType);
            foreach (var characteristic in itemCreateDto.ItemTypeCharacteristics)
            {
                var typeCharacteristicsModel = _mapper.Map<TypeCharacteristics>(characteristic.TypeCharacteristicName);
                var idTypeCharacteristic = await _typeCharacteristicsRepo.GetOrCreate(typeCharacteristicsModel);

                var exists = typeCharacteristicsItemValueModels?.Any(model => model.IdTypeCharacteristic == idTypeCharacteristic) ?? false;
                if (!exists)
                {
                    ItemsTypeCharacteristics itemsTypeCharacteristics = new ItemsTypeCharacteristics
                    {
                        IdItemType = idType,
                        IdTypeCharacteristic = idTypeCharacteristic,
                        TypeCharacteristicValue = characteristic.TypeCharacteristicValue
                    };
                    await _itemsTypeCharacteristicsRepo.Create(itemsTypeCharacteristics);
                }
            }

            var idItemPicture = await CreateItemPicture(itemCreateDto.ItemPicture);

            var skinModel = _mapper.Map<Skins>(itemCreateDto.SkinName);
            int idSkin = await _itemsSkinsRepo.GetOrCreate(skinModel);

            Items item = new Items
            {
                IdItemPicture = idItemPicture,
                IdSkin = idSkin,
                IdItemType = idType,
                Rarity = itemCreateDto.Rarity
            };

            int id = await _itemsRepo.CreateItem(item);
            return _mapper.Map<RecordIdReadDto>(id);
        }
        private async Task<int> CreateItemPicture(IFormFile itemPicture)
        {
            if (itemPicture != null && itemPicture.Length > 0)
            {
                if (!FileValidation.IsImageFile(itemPicture.FileName))
                {
                    throw new InvalidDataException("Invalid file type. Only image files are allowed!");
                }

                var picturePath = await SavePictureToFileSystem(itemPicture);
                int pictureId = await SavePicturePathToDatabase(picturePath);
                return pictureId;
            }
            else
            {
                throw new ArgumentNullException("Picture cannot be null!");
            }
        }
        private async Task<string> SavePictureToFileSystem(IFormFile picture)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), ItemsPicturesFolder);
            var pictureName = $"{Guid.NewGuid()}{Path.GetExtension(picture.FileName)}";
            var fullPicturePath = Path.Combine(uploadsFolder, pictureName);

            using (var stream = new FileStream(fullPicturePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            return Path.Combine(ItemsPicturesFolder, pictureName);
        }
        private async Task<int> SavePicturePathToDatabase(string picturePath)
        {
            var pictureModel = _mapper.Map<ItemsPictures>(picturePath);
            int id = await _itemsPicturesRepo.Create(pictureModel);
            return id;
        }

        public async Task DeleteItem(int id)
        {
            var item = await _itemsRepo.GetItemById(id);
            if(item == null)
            {
                throw new KeyNotFoundException("There is no Item with this ID!");
            }
            _itemsRepo.DeleteItem(item);
            await _itemsRepo.SaveChanges();
        }

        public async Task<IEnumerable<DealHistoryReadDto>> GetAllItemHistory(int idItem)
        {
            var items = await _dealsHistoryRepo.GetAllItemHistory(idItem) ?? throw new KeyNotFoundException("Items with your ID is not found!");
            return _mapper.Map<IEnumerable<DealHistoryReadDto>>(items);
        }
    }
}