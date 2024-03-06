#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Device.Api/Device.WebApi.csproj", "Device.Api/"]
COPY ["Device.Domain/Device.Domain.csproj", "Device.Domain/"]
COPY ["Device.Infrastructure/Device.Infrastructure.csproj", "Device.Infrastructure/"]
RUN dotnet restore "Device.Api/Device.WebApi.csproj"
COPY . .
WORKDIR "/src/Device.Api"
RUN dotnet build "Device.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Device.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Device.WebApi.dll"]