FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HappyDog.Console.Api/HappyDog.Console.Api.csproj", "HappyDog.Console.Api/"]
COPY ["HappyDog.Domain/HappyDog.Domain.csproj", "HappyDog.Domain/"]
COPY ["HappyDog.Infrastructure/HappyDog.Infrastructure.csproj", "HappyDog.Infrastructure/"]
RUN dotnet restore "HappyDog.Console.Api/HappyDog.Console.Api.csproj"
COPY . .
WORKDIR "/src/HappyDog.Console.Api"
RUN dotnet build "HappyDog.Console.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyDog.Console.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HappyDog.Console.Api.dll"]