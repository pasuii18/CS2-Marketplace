using AutoMapper;
using CSMarketApp.Domain.Core;
using CSMarketApp.Domain.Core.ItemTypeClassFolder;
using CSMarketApp.Domain.Core.ItemTypeFolder;
using CSMarketApp.Dtos;
using CSMarketApp.Dtos.DealsDtos;
using CSMarketApp.Dtos.ItemsDtos;
using CSMarketApp.Dtos.UsersDtos;

namespace CSMarketApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // source -> target
            CreateMap<Users, UserReadDto>();
            CreateMap<UserCreateDto, Users>();
            CreateMap<UserUpdateDto, Users>();

            // ITEMS MAPPING
            CreateMap<Items, ItemsReadDto>()
                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName))
                .ForMember(trg => trg.LowestPrice, opt => opt.MapFrom(src => src.Deals.Min(deal => (decimal?)deal.Price)));
                //.ForMember(trg => trg.LowestPrice, opt => opt.MapFrom(src => src.Deals.First().Price));

            // ITEM DETAILED MAPPPING
            CreateMap<Items, ItemDetailedReadDto>()
                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemsTypeCharacteristics, opt => opt.MapFrom(src => src.ItemsType.ItemsTypeCharacteristics))

                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemsClassCharacteristics, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsClassCharacteristics))

                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName))

                .ForMember(trg => trg.ItemDeals, opt => opt.MapFrom(src => src.Deals));

            CreateMap<Deals, ItemDealsReadDto>()
                .ForMember(trg => trg.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(trg => trg.Price, opt => opt.MapFrom(src => src.Price));

            // DEALS MAPPING
            CreateMap<Deals, DealReadDto>()
                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.Item.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemsTypeCharacteristics, opt => opt.MapFrom(src => src.Item.ItemsType.ItemsTypeCharacteristics))

                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.Item.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemsClassCharacteristics, opt => opt.MapFrom(src => src.Item.ItemsType.ItemsClass.ItemsClassCharacteristics))

                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.Item.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName))
                .ForMember(trg => trg.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(trg => trg.Price, opt => opt.MapFrom(src => src.Price));

            // CHARACTERISTICS MAPPING
            CreateMap<ItemsTypeCharacteristics, ItemsTypeCharacteristicsReadDto>()
                .ForMember(trg => trg.TypeCharacteristicName, opt => opt.MapFrom(src => src.TypeCharacteristic.TypeCharacteristicName))
                .ForMember(trg => trg.TypeCharacteristicValue, opt => opt.MapFrom(src => src.TypeCharacteristicValue));

            CreateMap<ItemsClassCharacteristics, ItemsClassCharacteristicsReadDto>()
                .ForMember(trg => trg.ClassCharacteristicName, opt => opt.MapFrom(src => src.ClassCharacteristic.ClassCharacteristicName))
                .ForMember(trg => trg.ClassCharacteristicValue, opt => opt.MapFrom(src => src.ClassCharacteristicValue));
        }
    }
}