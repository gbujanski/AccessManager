FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY ["AccessManager.sln", "./"]
COPY ["AccessManager.API/AccessManager.API.csproj", "AccessManager.API/"]
COPY ["AccessManager.Application/AccessManager.Application.csproj", "AccessManager.Application/"]
COPY ["AccessManager.Domain/AccessManager.Domain.csproj", "AccessManager.Domain/"]
COPY ["AccessManager.Infrastructure/AccessManager.Infrastructure.csproj", "AccessManager.Infrastructure/"]


RUN dotnet restore "AccessManager.sln"
COPY . .

WORKDIR "/src/AccessManager.API"
RUN dotnet publish "AccessManager.API.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "AccessManager.API.dll"]