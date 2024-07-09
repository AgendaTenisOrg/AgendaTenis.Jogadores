FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Ports/AgendaTenis.Jogadores.WebApi/AgendaTenis.Jogadores.WebApi.csproj", "src/Ports/AgendaTenis.Jogadores.WebApi/"]
COPY ["src/Core/AgendaTenis.Jogadores.Core/AgendaTenis.Jogadores.Core.csproj", "src/Core/AgendaTenis.Jogadores.Core/"]
RUN dotnet restore "./src/Ports/AgendaTenis.Jogadores.WebApi/AgendaTenis.Jogadores.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Ports/AgendaTenis.Jogadores.WebApi"
RUN dotnet build "./AgendaTenis.Jogadores.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AgendaTenis.Jogadores.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgendaTenis.Jogadores.WebApi.dll"]