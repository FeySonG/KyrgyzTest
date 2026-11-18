FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY KyrgyzTest.sln ./
COPY KyrgyzTest.Api/KyrgyzTest.Api.csproj KyrgyzTest.Api/
COPY KyrgyzTest.Application/KyrgyzTest.Application.csproj KyrgyzTest.Application/
COPY KyrgyzTest.Core/KyrgyzTest.Core.csproj KyrgyzTest.Core/
COPY KyrgyzTest.DAL/KyrgyzTest.DAL.csproj KyrgyzTest.DAL/
COPY KyrgyzTest.OldDb/KyrgyzTest.OldDb.csproj KyrgyzTest.OldDb/
COPY KyrgyzTest.OldDbRegion/KyrgyzTest.OldDbRegion.csproj KyrgyzTest.OldDbRegion/
RUN dotnet restore KyrgyzTest.Api/KyrgyzTest.Api.csproj
COPY . .
RUN dotnet publish KyrgyzTest.Api/KyrgyzTest.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KyrgyzTest.Api.dll"]
