using AutoMapper;
using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Infrastructure.Business.Algorithms;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealHistory;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos.Roles;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Authentication;
using CSMarketApp.Services.Interfaces.Dtos.DealsDtos.DealOffers;

namespace CSMarketApp.Infrastructure.Business.Services.UsersServices
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepo _usersRepo;
        private readonly IRolesRepo _rolesRepo;
        private readonly IDealsRepo _dealsRepo;
        private readonly IDealOffersRepo _offersRepo;
        private readonly IDealsHistoryRepo _dealsHistoryRepo;
        private readonly IUsersPicturesRepo _usersPicturesRepo;
        private readonly IMapper _mapper;
        private const string UsersPicturesFolder = "images\\UsersProfilePictures";

        public UsersService(IUsersRepo repo, IRolesRepo rolesRepo, IDealsRepo dealsRepo, IDealOffersRepo offersRepo, IDealsHistoryRepo dealsHistoryRepo, IUsersPicturesRepo usersPicturesRepo, IMapper mapper)
        {
            _usersRepo = repo;
            _rolesRepo = rolesRepo;
            _dealsRepo = dealsRepo;
            _offersRepo = offersRepo;
            _dealsHistoryRepo = dealsHistoryRepo;
            _usersPicturesRepo = usersPicturesRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDebugDto>> GetAllUsers()
        {
            var users = await _usersRepo.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDebugDto>>(users);
        }

        public async Task<UserProfileDto> GetUserByUUID(string uuid)
        {
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null)
            {
                throw new KeyNotFoundException("User with your UUID is not found!");
            }
            return _mapper.Map<UserProfileDto>(user);
        }
        public async Task UpdateUser([FromForm] UserUpdateDto userUpdateDto, string uuid)
        {
            if (userUpdateDto.NewLogin != null && await _usersRepo.GetUserByLogin(userUpdateDto.NewLogin) != null)
            {
                throw new DuplicateNameException("User with this login already exists!");
            }

            if (userUpdateDto.NewPassword != null)
            {
                userUpdateDto.NewPassword = Hashing.HashPassword(userUpdateDto.NewPassword);
            }

            var currentUser = await _usersRepo.GetUserByUUID(uuid);
            if (currentUser == null || !Hashing.VerifyHashedPassword(currentUser.Password, userUpdateDto.OldPassword))
            {
                throw new AuthenticationException("Password is incorrect!");
            }

            await UpdateUserProfilePicture(userUpdateDto, currentUser);

            _mapper.Map(userUpdateDto, currentUser);
            await _usersRepo.SaveChanges();
        }
        public async Task DeleteUser(UserAuthDto userAuthDto, string uuid)
        {
            var user = await _usersRepo.GetUserByUUID(uuid);
            if(user == null)
            {
                throw new AuthenticationException("User with this UUID is not found!");
            }
            if (user.Login != userAuthDto.Login || !Hashing.VerifyHashedPassword(user.Password, userAuthDto.Password))
            {
                throw new AuthenticationException("Login or Password is incorrect!");
            }

            await DeleteUserPictureIfExists(user.IdUserProfilePicture);

            _usersRepo.DeleteUser(user);
            await _usersRepo.SaveChanges();
        }
        public async Task UpdateUserRole(UserRoleUpdateDto userRoleUpdateDto)
        {
            var user = await _usersRepo.GetUserByUUID(userRoleUpdateDto.UUID);
            if (user == null)
            {
                throw new KeyNotFoundException("User with this UUID not found!");
            }

            var role = await _rolesRepo.GetRoleByName(userRoleUpdateDto.RoleName);
            if (user == null)
            {
                throw new KeyNotFoundException("Role with this name not found!");
            }

            user.IdRole = role.IdRole;
            await _usersRepo.SaveChanges();
        }
        public async Task<IEnumerable<DealReadDto>> GetUserDeals(string uuid)
        {
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null)
            {
                throw new KeyNotFoundException("User with your UUID is not found!");
            }
            var userDeals = await _dealsRepo.GetUserRows(user.IdUser);
            return _mapper.Map<IEnumerable<DealReadDto>>(userDeals);
        }
        public async Task<IEnumerable<DealOffersReadDto>> GetUserOffers(string uuid)
        {
            var user = await _usersRepo.GetUserByUUID(uuid);
            if (user == null)
            {
                throw new KeyNotFoundException("User with your UUID is not found!");
            }
            var userOffers = await _offersRepo.GetAllByUserId(user.IdUser);
            return _mapper.Map<IEnumerable<DealOffersReadDto>>(userOffers);
        }
        public async Task<IEnumerable<UserDealsHistoryReadDto>> GetAllUserSellingHistory(string sellerUUID)
        {
            var user = await _usersRepo.GetUserByUUID(sellerUUID) ?? throw new KeyNotFoundException("User with your UUID in not found!");
            var sellingHistory = await _dealsHistoryRepo.GetAllUserSellingHistory(user.IdUser) ?? throw new KeyNotFoundException("There is no sold items!");
            return _mapper.Map<IEnumerable<UserDealsHistoryReadDto>>(sellingHistory);
        }
        public async Task<IEnumerable<UserDealsHistoryReadDto>> GetAllUserBuyingHistory(string buyerUUID)
        {
            var user = await _usersRepo.GetUserByUUID(buyerUUID) ?? throw new KeyNotFoundException("User with your UUID in not found!");
            var buyingHistory = await _dealsHistoryRepo.GetAllUserBuyingHistory(user.IdUser) ?? throw new KeyNotFoundException("There is no buyed items!");
            return _mapper.Map<IEnumerable<UserDealsHistoryReadDto>>(buyingHistory);
        }

        private async Task UpdateUserProfilePicture(UserUpdateDto userUpdateDto, Users currentUser)
        {
            if (userUpdateDto.UserPicture != null && userUpdateDto.UserPicture.Length > 0)
            {
                if (!FileValidation.IsImageFile(userUpdateDto.UserPicture.FileName))
                {
                    throw new InvalidDataException("Invalid file type. Only image files are allowed!");
                }

                await DeleteUserPictureIfExists(currentUser.IdUserProfilePicture);

                var picturePath = await SavePictureToFileSystem(userUpdateDto.UserPicture);
                int pictureId = await SavePicturePathToDatabase(picturePath);
                userUpdateDto.IdUserProfilePicture = pictureId;
            }
        }
        private async Task<string> SavePictureToFileSystem(IFormFile picture)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), UsersPicturesFolder);
            var pictureName = $"{Guid.NewGuid()}{Path.GetExtension(picture.FileName)}";
            var fullPicturePath = Path.Combine(uploadsFolder, pictureName);

            using (var stream = new FileStream(fullPicturePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            return Path.Combine(UsersPicturesFolder, pictureName);
        }
        private async Task<int> SavePicturePathToDatabase(string picturePath)
        {
            var pictureModel = _mapper.Map<UsersPictures>(picturePath);
            int id = await _usersPicturesRepo.Create(pictureModel);
            await _usersPicturesRepo.SaveChanges();
            return id;
        }
        private async Task DeleteUserPictureIfExists(int? pictureId)
        {
            if (pictureId == null) return;

            var picture = await _usersPicturesRepo.GetById(pictureId.Value);
            if (picture == null) return;

            _usersPicturesRepo.Delete(picture);
            await _usersPicturesRepo.SaveChanges();

            var picturePath = Path.Combine(Directory.GetCurrentDirectory(), picture.PicturePath);
            if (File.Exists(picturePath))
            {
                File.Delete(picturePath);
            }
        }
    }
}