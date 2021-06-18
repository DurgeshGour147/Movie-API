FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

ENV TZ="Asia/Kolkata"

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /usr/lib/ssl/openssl.cnf

EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY Movie.sln ./
COPY Movie/*.csproj ./Movie/
COPY Movie.Common/*.csproj Movie.Common/
COPY Movie.Domain/*.csproj Movie.Domain/
COPY Movie.DTO/*.csproj Movie.DTO/
COPY Movie.Interface/Movie.ProviderInterface/*.csproj Movie.Interface/Movie.ProviderInterface/
COPY Movie.Interface/Movie.RepositoryInterface/*.csproj Movie.Interface/Movie.RepositoryInterface/
COPY Movie.Provider/*.csproj Movie.Provider/
COPY Movie.Repository/Movie.CacheRepository/*.csproj Movie.Repository/Movie.CacheRepository/
COPY Movie.Repository/Movie.DBRepository/*.csproj Movie.Repository/Movie.DBRepository/

RUN dotnet restore
COPY . .
WORKDIR /src/Movie
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Common
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Domain
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.DTO
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Interface/Movie.ProviderInterface
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Interface/Movie.RepositoryInterface
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Provider
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Repository/Movie.CacheRepository
RUN dotnet publish -c Release -o /app

WORKDIR /src/Movie.Repository/Movie.DBRepository
RUN dotnet publish -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app/ .
ENTRYPOINT ["dotnet", "Movie.dll"]