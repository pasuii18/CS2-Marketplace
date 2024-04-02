## A web application that emulates the operation of the trading platform for CS2 game items. 
Performed as part of a university course project.

Technologies:
* Onion architecture
* Exception Middleware
* Blazor UI
* Code-First using EF
* MySql with 16 tables on Docker
* JWT

### How To Start?

**Run all commands in the _root_ directory!!!**

## 1. Create docker container
Use `docker-compose up -d` to create container

## 2. Create migration
Use `dotnet ef migrations add _<migration_name>_ --project .\CSMarketApp.Infrastructure.Data\ --startup-project CSMarketApp` to create migration

## 3. Update Database
Use `dotnet ef database update --project .\CSMarketApp.Infrastructure.Data\ --startup-project CSMarketApp` to update database

### Usefull commands:
1. Use `--verbose` flag for detailed information about EF processes
2. Use `dotnet ef database drop --project .\CSMarketApp.Infrastructure.Data\ --startup-project CSMarketApp` to drop database

Stay happy and stay hydrated! :3
