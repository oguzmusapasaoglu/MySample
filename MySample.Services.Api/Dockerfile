#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MySample.Services.Api/MySample.Services.Api.csproj", "MySample.Services.Api/"]
RUN dotnet restore "MySample.Services.Api/MySample.Services.Api.csproj"
COPY . .
WORKDIR "/src/MySample.Services.Api"
RUN dotnet build "MySample.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MySample.Services.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MySample.Services.Api.dll"]