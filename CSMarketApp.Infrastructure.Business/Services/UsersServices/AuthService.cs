using AutoMapper;
using CSMarketApp.Domain.Core.Models;
using CSMarketApp.Domain.Core.Models.UsersModels;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Infrastructure.Business.Algorithms;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using FluentValidation;

namespace CSMarketApp.Infrastructure.Business.Services.UsersServices
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepo _repo;
        private readonly IMapper _mapper;
        private readonly JWTSettings _options;
        private readonly IValidator<UserCreateDto> _userValidator;

        public AuthService(IUsersRepo repo, IMapper mapper, IOptions<JWTSettings> optAccess, IValidator<UserCreateDto> userValidator)
        {
            _repo = repo;
            _mapper = mapper;
            _options = optAccess.Value;
            _userValidator = userValidator;
        }

        public async Task<string> GenerateJwt(UserProfileDto userReadDto)
        {
            var claims = new List<Claim>
            {
                new Claim("uuid", userReadDto.UUID),
                new Claim(ClaimTypes.Role, userReadDto.Role)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(120)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public async Task<string> Authentication(UserAuthDto userAuthDto)
        {
            var user = await _repo.GetUserByLogin(userAuthDto.Login);
            if (user == null || !Hashing.VerifyHashedPassword(user.Password, userAuthDto.Password))
            {
                throw new AuthenticationException("Login or Password is incorrect!");
            }

            var userReadDto = _mapper.Map<UserProfileDto>(user);
            var token = await GenerateJwt(userReadDto);

            return token;
        }
        public async Task<string> Registration(UserCreateDto userCreateDto)
        {
            var validationResults = await _userValidator.ValidateAsync(userCreateDto);
            if (!validationResults.IsValid)
            {
                throw new ValidationException(validationResults.Errors);
            }
            
            var user = await _repo.GetUserByLogin(userCreateDto.Login);
            if (user != null)
            {
                throw new DuplicateNameException("User with this login already exists!");
            }

            var userModel = _mapper.Map<Users>(userCreateDto);
            userModel.UUID = UUIDGenerating.GenerateUUID();
            userModel.Password = Hashing.HashPassword(userModel.Password);

            await _repo.CreateUser(userModel);
            await _repo.SaveChanges();

            var userReadDto = _mapper.Map<UserProfileDto>(userModel);
            var token = await GenerateJwt(userReadDto);

            return token;
        }
    }
}