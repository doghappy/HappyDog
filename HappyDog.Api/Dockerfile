FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
COPY ["HappyDog.Api/HappyDog.Api.csproj", "HappyDog.Api/"]
COPY ["HappyDog.Infrastructure/HappyDog.Infrastructure.csproj", "HappyDog.Infrastructure/"]
COPY ["HappyDog.Domain/HappyDog.Domain.csproj", "HappyDog.Domain/"]
RUN dotnet restore "HappyDog.Api/HappyDog.Api.csproj"
COPY . .
WORKDIR "/src/HappyDog.Api"
RUN dotnet build "HappyDog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyDog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HappyDog.Api.dll"]