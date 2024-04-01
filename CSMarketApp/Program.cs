using CSMarketApp.Domain.Core.Models;
using CSMarketApp.Domain.Core.Models.ItemsModels.Class;
using CSMarketApp.Domain.Core.Models.ItemsModels.SubClass;
using CSMarketApp.Domain.Core.Models.ItemsModels.Type;
using CSMarketApp.Domain.Interfaces.ReposInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.DealsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.ItemsInterfaces;
using CSMarketApp.Domain.Interfaces.ReposInterfaces.UsersInterfaces;
using CSMarketApp.Infrastructure.Business.Services.DealsServices;
using CSMarketApp.Infrastructure.Business.Services.ItemsServices;
using CSMarketApp.Infrastructure.Business.Services.ItemsServices.Class;
using CSMarketApp.Infrastructure.Business.Services.ItemsServices.SubClass;
using CSMarketApp.Infrastructure.Business.Services.ItemsServices.Type;
using CSMarketApp.Infrastructure.Business.Services.UsersServices;
using CSMarketApp.Infrastructure.Data;
using CSMarketApp.Infrastructure.Data.Repos.DealsRepos;
using CSMarketApp.Infrastructure.Data.Repos.ItemsRepos;
using CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Class;
using CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.SubClass;
using CSMarketApp.Infrastructure.Data.Repos.ItemsRepos.Type;
using CSMarketApp.Infrastructure.Data.Repos.UsersRepos;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ClassCharacteristics;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Class.ItemsClass;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.SubClass.ItemsSubClass;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.ItemsType;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos.Type.TypeCharacteristics;
using CSMarketApp.Services.Interfaces.ServicesInterfaces;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.DealsInterfaces;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.ItemsInterfaces;
using CSMarketApp.Services.Interfaces.ServicesInterfaces.UsersInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CSMarketApp.Infrastructure.Business.Algorithms;
using CSMarketApp.Services.Interfaces.Dtos.UsersDtos;
using FluentValidation;

namespace CSMarketApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var sqlConBuilder = new SqlConnectionStringBuilder();
            sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
            sqlConBuilder.UserID = builder.Configuration["UserId"];
            sqlConBuilder.Password = builder.Configuration["Password"];

            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));
            builder.Services.AddScoped<IUsersRepo, UsersRepo>();
            builder.Services.AddScoped<IDealsRepo, DealsRepo>();
            builder.Services.AddScoped<IItemsRepo, ItemsRepo>();
            builder.Services.AddScoped<IUsersPicturesRepo, UsersPicturesRepo>();
            builder.Services.AddScoped<IDealOffersRepo, DealOffersRepo>();
            builder.Services.AddScoped<IRolesRepo, RolesRepo>();
            builder.Services.AddScoped<IDealsHistoryRepo, DealsHistoryRepo>();
            builder.Services.AddScoped<IItemsPicturesRepo, ItemsPicturesRepo>();
            builder.Services.AddScoped<IItemsSkinsRepo, ItemsSkinsRepo>();
            builder.Services.AddScoped<IItemsClassCharacteristicsRepo, ItemsClassCharacteristicsRepo>();
            builder.Services.AddScoped<IItemsTypeCharacteristicsRepo, ItemsTypeCharacteristicsRepo>();
            builder.Services.AddScoped<IItemsSkinsRepo, ItemsSkinsRepo>();

            builder.Services.AddScoped<ITemplateRepo<ItemsSubClass>, ItemsSubClassRepo>();
            builder.Services.AddScoped<ITemplateRepo<ItemsClass>, ItemsClassRepo>();
            builder.Services.AddScoped<ITemplateRepo<ClassCharacteristics>, ClassCharacteristicsRepo>();
            builder.Services.AddScoped<ITemplateRepo<ItemsType>, ItemsTypeRepo>();
            builder.Services.AddScoped<ITemplateRepo<TypeCharacteristics>, TypeCharacteristicsRepo>();

            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IItemsService, ItemsService>();
            builder.Services.AddScoped<IUsersPicturesService, UsersPicturesService>();
            builder.Services.AddScoped<IDealsService, DealsService>();
            builder.Services.AddScoped<IDealOffersService, DealOffersService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IDealsHistoryService, DealsHistoryService>();
            builder.Services.AddScoped<IItemsPicturesService, ItemsPicturesService>();
            builder.Services.AddScoped<IItemsClassCharacteristicsService, ItemsClassCharacteristicsService>();
            builder.Services.AddScoped<IItemsTypeCharacteristicsService, ItemsTypeCharacteristicsService>();
            builder.Services.AddScoped<IItemsSkinsService, ItemsSkinsService>();

            builder.Services.AddScoped<ITemplateService<ItemsSubClassReadDto, ItemsSubClassCreateDto, ItemsSubClassUpdateDto>, ItemsSubClassService>();
            builder.Services.AddScoped<ITemplateService<ItemsClassReadDto, ItemsClassCreateDto, ItemsClassUpdateDto>, ItemsClassService>();
            builder.Services.AddScoped<ITemplateService<ClassCharacteristicsReadDto, ClassCharacteristicsCreateDto, ClassCharacteristicsUpdateDto>, ClassCharacteristicsService>();
            builder.Services.AddScoped<ITemplateService<ItemsTypeReadDto, ItemsTypeCreateDto, ItemsTypeUpdateDto>, ItemsTypeService>();
            builder.Services.AddScoped<ITemplateService<TypeCharacteristicsReadDto, TypeCharacteristicsCreateDto, TypeCharacteristicsUpdateDto>, TypeCharacteristicsService>();

            builder.Services.AddScoped<IValidator<UserCreateDto>, UserValidator>();
            builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWTSettings"));

            builder.Services.AddScoped<ExceptionHandlerMiddleware>();

            builder.Services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins()
                   .AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            }));

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var secretKey = builder.Configuration.GetSection("JWTSettings:SecretKey").Value;
            var issuer = builder.Configuration.GetSection("JWTSettings:Issuer").Value;
            var audience = builder.Configuration.GetSection("JWTSettings:Audience").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                };
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            
            app.UseWebSockets();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}