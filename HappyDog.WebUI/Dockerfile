FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["HappyDog.WebUI/HappyDog.WebUI.csproj", "HappyDog.WebUI/"]
COPY ["HappyDog.Domain/HappyDog.Domain.csproj", "HappyDog.Domain/"]
COPY ["HappyDog.Infrastructure/HappyDog.Infrastructure.csproj", "HappyDog.Infrastructure/"]
RUN dotnet restore "HappyDog.WebUI/HappyDog.WebUI.csproj"
COPY . .
WORKDIR "/src/HappyDog.WebUI"
RUN dotnet build "HappyDog.WebUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HappyDog.WebUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HappyDog.WebUI.dll"]