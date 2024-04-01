using AutoMapper;
using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ClassCharacteristics;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClass;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClassCharacteristics;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Skins;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.SubClass.ItemsSubClass;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsType;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsTypeCharacteristics;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.TypeCharacteristics;

namespace CSMarketApp.Infrastructure.Business.Mapping
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile() 
        {
            // ITEMS CREATE MAPPING
            CreateMap<string, ItemsPictures>()
                .ForMember(trg => trg.ItemPicturePath, opt => opt.MapFrom(src => src));
            CreateMap<string, Skins>()
                .ForMember(trg => trg.SkinName, opt => opt.MapFrom(src => src));
            CreateMap<string, ItemsSubClass>()
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src));
            CreateMap<string, ClassCharacteristics>()
                .ForMember(trg => trg.ClassCharacteristicName, opt => opt.MapFrom(src => src));
            CreateMap<string, TypeCharacteristics>()
                .ForMember(trg => trg.TypeCharacteristicName, opt => opt.MapFrom(src => src));

            // ITEMS_SUB_CLASS MAPPING
            CreateMap<ItemsSubClass, ItemsSubClassReadDto>();
            CreateMap<ItemsSubClassCreateDto, ItemsSubClass>();
            CreateMap<ItemsSubClassUpdateDto, ItemsSubClass>();

            // ITEMS_CLASS MAPPING
            CreateMap<ItemsClass, ItemsClassReadDto>()
                .ForMember(trg => trg.IdItemClass, opt => opt.MapFrom(src => src.IdItemClass))
                .ForMember(trg => trg.IdItemSubClass, opt => opt.MapFrom(src => src.IdItemSubClass))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemsSubClass.ItemSubClassName));
            CreateMap<ItemsClassCreateDto, ItemsClass>();
            CreateMap<ItemsClassUpdateDto, ItemsClass>()
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null && member.ToString() != "0"));

            CreateMap<ClassCharacteristics, ClassCharacteristicsReadDto>();
            CreateMap<ClassCharacteristicsCreateDto, ClassCharacteristics>();
            CreateMap<ClassCharacteristicsUpdateDto, ClassCharacteristics>();

            CreateMap<ItemsClassCharacteristics, ItemsClassCharacteristicsReadDto>()
                .ForMember(trg => trg.IdItemClass, opt => opt.MapFrom(src => src.IdItemClass))
                .ForMember(trg => trg.IdClassCharacteristic, opt => opt.MapFrom(src => src.IdClassCharacteristic))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemClass.ItemsSubClass.ItemSubClassName))
                .ForMember(trg => trg.ClassCharacteristicName, opt => opt.MapFrom(src => src.ClassCharacteristic.ClassCharacteristicName))
                .ForMember(trg => trg.ClassCharacteristicValue, opt => opt.MapFrom(src => src.ClassCharacteristicValue));
            CreateMap<ItemsClassCharacteristicsCreateDto, ItemsClassCharacteristics>();
            CreateMap<ItemsClassCharacteristicsUpdateDto, ItemsClassCharacteristics>();

            // ITEMS_TYPE MAPPING
            CreateMap<ItemsType, ItemsTypeReadDto>()
                .ForMember(trg => trg.IdItemType, opt => opt.MapFrom(src => src.IdItemType))
                .ForMember(trg => trg.IdItemClass, opt => opt.MapFrom(src => src.IdItemClass))
                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemTypeName))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClasName, opt => opt.MapFrom(src => src.ItemsClass.ItemsSubClass.ItemSubClassName));
            CreateMap<ItemsTypeCreateDto, ItemsType>();
            CreateMap<ItemsTypeUpdateDto, ItemsType>()
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null && member.ToString() != "0"));

            CreateMap<TypeCharacteristics, TypeCharacteristicsReadDto>();
            CreateMap<TypeCharacteristicsCreateDto, TypeCharacteristics>();
            CreateMap<TypeCharacteristicsUpdateDto, TypeCharacteristics>();

            CreateMap<ItemsTypeCharacteristics, ItemsTypeCharacteristicsReadDto>()
                .ForMember(trg => trg.IdItemType, opt => opt.MapFrom(src => src.IdItemType))
                .ForMember(trg => trg.IdTypeCharacteristic, opt => opt.MapFrom(src => src.IdTypeCharacteristic))
                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemType.ItemTypeName))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemType.ItemsClass.ItemsSubClass.ItemSubClassName))
                .ForMember(trg => trg.TypeCharacteristicName, opt => opt.MapFrom(src => src.TypeCharacteristic.TypeCharacteristicName))
                .ForMember(trg => trg.TypeCharacteristicValue, opt => opt.MapFrom(src => src.TypeCharacteristicValue));
            CreateMap<ItemsTypeCharacteristicsCreateDto, ItemsTypeCharacteristics>();
            CreateMap<ItemsTypeCharacteristicsUpdateDto, ItemsTypeCharacteristics>();

            // ITEMS MAPPING
            CreateMap<ItemsPictures, ItemsPicturesReadDto>();

            CreateMap<ItemCreateDto, Items>();

            CreateMap<ItemsTypeCreateDto, ItemsType>();
            CreateMap<TypeCharacteristicsCreateDto, TypeCharacteristics>();
            CreateMap<ItemsTypeCharacteristicsCreateDto, ItemsTypeCharacteristics>();

            CreateMap<ItemsClassCreateDto, ItemsClass>();
            CreateMap<ClassCharacteristicsCreateDto, ClassCharacteristics>();
            CreateMap<ItemsClassCharacteristicsCreateDto, ItemsClassCharacteristics>();

            CreateMap<ItemsSubClassCreateDto, ItemsSubClass>();

            CreateMap<Items, ItemsReadDto>()
                .ForMember(trg => trg.IdItem, opt => opt.MapFrom(src => src.IdItem))
                .ForMember(trg => trg.IdItemPicture, opt => opt.MapFrom(src => src.IdItemPicture))
                .ForMember(trg => trg.SkinName, opt => opt.MapFrom(src => src.Skin.SkinName))
                .ForMember(trg => trg.Rarity, opt => opt.MapFrom(src => src.Rarity))

                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName))
                .ForMember(trg => trg.LowestPrice, opt => opt.MapFrom(src => src.Deals.Min(deal => (decimal?)deal.Price)));

            // ITEM DETAILED MAPPPING
            CreateMap<Items, ItemDetailedReadDto>()
                .ForMember(trg => trg.IdItem, opt => opt.MapFrom(src => src.IdItem))
                .ForMember(trg => trg.IdItemPicture, opt => opt.MapFrom(src => src.IdItemPicture))
                .ForMember(trg => trg.SkinName, opt => opt.MapFrom(src => src.Skin.SkinName))
                .ForMember(trg => trg.Rarity, opt => opt.MapFrom(src => src.Rarity))

                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemsTypeCharacteristics, opt => opt.MapFrom(src => src.ItemsType.ItemsTypeCharacteristics))

                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemsClassCharacteristics, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsClassCharacteristics))

                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName))

                .ForMember(trg => trg.ItemDeals, opt => opt.MapFrom(src => src.Deals));

            // CHARACTERISTICS MAPPING
            CreateMap<ItemsTypeCharacteristics, TypeCharacteristicsValuesReadDto>()
                .ForMember(trg => trg.TypeCharacteristicName, opt => opt.MapFrom(src => src.TypeCharacteristic.TypeCharacteristicName))
                .ForMember(trg => trg.TypeCharacteristicValue, opt => opt.MapFrom(src => src.TypeCharacteristicValue));

            CreateMap<ItemsClassCharacteristics, ClassCharacteristicsValuesReadDto>()
                .ForMember(trg => trg.ClassCharacteristicName, opt => opt.MapFrom(src => src.ClassCharacteristic.ClassCharacteristicName))
                .ForMember(trg => trg.ClassCharacteristicValue, opt => opt.MapFrom(src => src.ClassCharacteristicValue));
            
            // Skins
            CreateMap<Skins, SkinsReadDto>();
            CreateMap<SkinsCreateDto, Skins>();
            CreateMap<SkinsUpdateDto, Skins>();
        }
    }
}
