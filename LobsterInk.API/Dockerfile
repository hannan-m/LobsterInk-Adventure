#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LobsterInk.API/LobsterInk.API.csproj", "LobsterInk.API/"]
COPY ["LobsterInk.Application/LobsterInk.Application.csproj", "LobsterInk.Application/"]
COPY ["LobsterInk.Domain/LobsterInk.Domain.csproj", "LobsterInk.Domain/"]
COPY ["LobsterInk.Infrastructure/LobsterInk.Infrastructure.csproj", "LobsterInk.Infrastructure/"]
RUN dotnet restore "LobsterInk.API/LobsterInk.API.csproj"
COPY . .
WORKDIR "/src/LobsterInk.API"
RUN dotnet build "LobsterInk.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LobsterInk.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LobsterInk.API.dll"]