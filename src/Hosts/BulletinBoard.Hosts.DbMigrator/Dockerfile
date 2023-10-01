#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Hosts/BulletinBoard.Hosts.DbMigrator/*.csproj", "src/Hosts/BulletinBoard.Hosts.DbMigrator/"]
COPY ["src/Infrastructure/BulletinBoard.Infrastructure.DataAccess/*.csproj", "src/Infrastructure/BulletinBoard.Infrastructure.DataAccess/"]
COPY ["src/Application/BulletinBoard.Application.AppServices/*.csproj", "src/Application/BulletinBoard.Application.AppServices/"]
COPY ["src/Contracts/BulletinBoard.Contracts/*.csproj", "src/Contracts/BulletinBoard.Contracts/"]
COPY ["src/Domain/BulletinBoard.Domain/*.csproj", "src/Domain/BulletinBoard.Domain/"]
COPY ["src/Infrastructure/BulletinBoard.Infrastructure/*.csproj", "src/Infrastructure/BulletinBoard.Infrastructure/"]
RUN dotnet restore "src/Hosts/BulletinBoard.Hosts.DbMigrator/BulletinBoard.Hosts.DbMigrator.csproj"
COPY . .
WORKDIR "/src/src/Hosts/BulletinBoard.Hosts.DbMigrator"
RUN dotnet build "BulletinBoard.Hosts.DbMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulletinBoard.Hosts.DbMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulletinBoard.Hosts.DbMigrator.dll"]