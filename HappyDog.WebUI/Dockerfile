FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
#RUN apt-get update \
    #&& apt-get install -y --allow-unauthenticated \
        #libc6-dev \
        #libgdiplus \
        #libx11-dev \
     #&& rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["HappyDog.WebUI/HappyDog.WebUI.csproj", "HappyDog.WebUI/"]
COPY ["HappyDog.Domain/HappyDog.Domain.csproj", "HappyDog.Domain/"]
COPY ["HappyDog.Infrastructure/HappyDog.Infrastructure.csproj", "HappyDog.Infrastructure/"]
RUN dotnet restore "HappyDog.WebUI/HappyDog.WebUI.csproj"
COPY . .
WORKDIR "/src/HappyDog.WebUI"
RUN dotnet build "HappyDog.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HappyDog.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HappyDog.WebUI.dll"]