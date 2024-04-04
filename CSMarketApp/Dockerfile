FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CSMarketApp/CSMarketApp.csproj", "CSMarketApp/"]
COPY ["CSMarketApp.Domain.Interfaces/CSMarketApp.Domain.Interfaces.csproj", "CSMarketApp.Domain.Interfaces/"]
COPY ["CSMarketApp.Domain.Core/CSMarketApp.Domain.Core.csproj", "CSMarketApp.Domain.Core/"]
COPY ["CSMarketApp.Infrastructure.Business/CSMarketApp.Infrastructure.Business.csproj", "CSMarketApp.Infrastructure.Business/"]
COPY ["CSMarketApp.Infrastructure.Data/CSMarketApp.Infrastructure.Data.csproj", "CSMarketApp.Infrastructure.Data/"]
COPY ["CSMarketApp.Services.Interfaces/CSMarketApp.Services.Interfaces.csproj", "CSMarketApp.Services.Interfaces/"]
RUN dotnet restore "CSMarketApp/CSMarketApp.csproj"
COPY . .
WORKDIR "/src/CSMarketApp"
RUN dotnet build "CSMarketApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CSMarketApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSMarketApp.dll"]
