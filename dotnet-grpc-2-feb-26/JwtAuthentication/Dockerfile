﻿FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["JwtAuthentication/JwtAuthentication.csproj", "JwtAuthentication/"]
RUN dotnet restore "JwtAuthentication/JwtAuthentication.csproj"
COPY . .
WORKDIR "/src/JwtAuthentication"
RUN dotnet build "JwtAuthentication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JwtAuthentication.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JwtAuthentication.dll"]
