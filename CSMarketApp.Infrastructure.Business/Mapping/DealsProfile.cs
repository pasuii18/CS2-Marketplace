using AutoMapper;
using CSMarketApp.Domain.Core.Models.DealsModels;
using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Services.Interfaces.Dtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;

namespace CSMarketApp.Infrastructure.Business.Mapping
{
    public class DealsProfile : Profile
    {
        public DealsProfile()
        {
            // source -> target

            CreateMap<Deals, ItemDealsReadDto>()
                .ForMember(trg => trg.IdUserProfilePicture, opt => opt.MapFrom(src => src.User.IdUserProfilePicture))
                .ForMember(trg => trg.UUID, opt => opt.MapFrom(src => src.User.UUID))
                .ForMember(trg => trg.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(trg => trg.Price, opt => opt.MapFrom(src => src.Price));

            //DEALS OFFERS
            CreateMap<DealOffers, DealOffersReadDto>()
                .ForMember(trg => trg.OffererUUID, opt => opt.MapFrom(src => src.Offerer.UUID));
            CreateMap<DealOffersCreateDto, DealOffers>();
            CreateMap<DealOffersUpdateDto, DealOffers>();

            //DEALS HISTORY
            CreateMap<DealsHistory, UserDealsHistoryReadDto>()
                .ForMember(trg => trg.Item, opt => opt.MapFrom(src => src.Item))
                .ForMember(trg => trg.SellerUUID, opt => opt.MapFrom(src => src.Seller.UUID))
                .ForMember(trg => trg.BuyerUUID, opt => opt.MapFrom(src => src.Buyer.UUID))
                .ForMember(trg => trg.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(trg => trg.Date, opt => opt.MapFrom(src => src.Date));

            // fix
            CreateMap<Items, ItemDealsHistoryReadDto>()
                .ForMember(trg => trg.IdItem, opt => opt.MapFrom(src => src.IdItem))
                .ForMember(trg => trg.Rarity, opt => opt.MapFrom(src => src.Rarity))
                .ForMember(trg => trg.SkinName, opt => opt.MapFrom(src => src.Skin.SkinName))
                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName));

            CreateMap<DealsHistory, DealHistoryReadDto>()
                .ForMember(trg => trg.IdItem, opt => opt.MapFrom(src => src.IdItem))
                .ForMember(trg => trg.SellerUUID, opt => opt.MapFrom(src => src.Seller.UUID))
                .ForMember(trg => trg.BuyerUUID, opt => opt.MapFrom(src => src.Buyer.UUID))
                .ForMember(trg => trg.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(trg => trg.Date, opt => opt.MapFrom(src => src.Date));

            // DEALS MAPPING
            CreateMap<DealCreateDto, Deals>();
            CreateMap<Deals, DealReadDto>()
                .ForMember(trg => trg.IdDeal, opt => opt.MapFrom(src => src.IdDeal))
                .ForMember(trg => trg.IdItem, opt => opt.MapFrom(src => src.Item.IdItem))
                .ForMember(trg => trg.IdItemPicture, opt => opt.MapFrom(src => src.Item.IdItemPicture))
                .ForMember(trg => trg.SkinName, opt => opt.MapFrom(src => src.Item.Skin.SkinName))
                .ForMember(trg => trg.Rarity, opt => opt.MapFrom(src => src.Item.Rarity))

                .ForMember(trg => trg.ItemTypeName, opt => opt.MapFrom(src => src.Item.ItemsType.ItemTypeName))
                .ForMember(trg => trg.ItemClassName, opt => opt.MapFrom(src => src.Item.ItemsType.ItemsClass.ItemClassName))
                .ForMember(trg => trg.ItemSubClassName, opt => opt.MapFrom(src => src.Item.ItemsType.ItemsClass.ItemsSubClass.ItemSubClassName))

                .ForMember(trg => trg.IdUserPicture, opt => opt.MapFrom(src => src.User.IdUserProfilePicture))
                .ForMember(trg => trg.UserUUID, opt => opt.MapFrom(src => src.User.UUID))
                .ForMember(trg => trg.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(trg => trg.Price, opt => opt.MapFrom(src => src.Price));

            // RECORD_ID MAPPING
            CreateMap<int, RecordIdReadDto>()
                .ForMember(trg => trg.RecordId, opt => opt.MapFrom(src => src));
        }
    }
}
