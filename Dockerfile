#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Reto.API/Reto.API.csproj", "Reto.API/"]
COPY ["Reto.Application/Reto.Application.csproj", "Reto.Application/"]
COPY ["Reto.Domain/Reto.Domain.csproj", "Reto.Domain/"]
COPY ["Reto.Resource/Reto.Resource.csproj", "Reto.Resource/"]
COPY ["Reto.Infraestructure/Reto.Infraestructure.csproj", "Reto.Infraestructure/"]
RUN dotnet restore "Reto.API/Reto.API.csproj"
COPY . .
WORKDIR "/src/Reto.API"
RUN dotnet build "Reto.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reto.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reto.API.dll"]