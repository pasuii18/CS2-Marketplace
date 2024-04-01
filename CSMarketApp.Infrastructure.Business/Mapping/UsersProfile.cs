using AutoMapper;
using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Pictures;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;

namespace CSMarketApp.Infrastructure.Business.Mapping
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Users, UserDebugDto>()
                .ForMember(trg => trg.IdPicture, opt => opt.MapFrom(src => src.UserProfilePicture.IdUserProfilePicture))
                .ForMember(trg => trg.Role, opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<Users, UserProfileDto>()
                .ForMember(trg => trg.IdPicture, opt => opt.MapFrom(src => src.UserProfilePicture.IdUserProfilePicture))
                .ForMember(trg => trg.Role, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : "Member"));

            CreateMap<string, UsersPictures>()
                .ForMember(dest => dest.PicturePath, opt => opt.MapFrom(src => src));

            CreateMap<UserCreateDto, Users>();

            CreateMap<UserUpdateDto, Users>()
                .ForAllMembers(opt => opt.Condition((src, dest, member) => member != null));

            CreateMap<UserAuthDto, Users>();

            CreateMap<UsersPictures, UsersPicturesReadDto>();

            // ROLES
            CreateMap<Roles,RolesReadDto>();
            CreateMap<RolesCreateDto, Roles>();
            CreateMap<RolesUpdateDto, Roles>()
                .ForMember(trg => trg.RoleName, opt => opt.MapFrom(src => src.NewRoleName));
        }
    }
}
