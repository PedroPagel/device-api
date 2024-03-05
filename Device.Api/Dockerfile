#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
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