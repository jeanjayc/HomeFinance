FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/HomeFinance.Api/HomeFinance.Api.csproj", "src/HomeFinance.Api/"]
COPY ["src/HomeFinance.Application/HomeFinance.Application.csproj", "src/HomeFinance.Application/"]
COPY ["src/HomeFinance.Infra/HomeFinance.Infra.csproj", "src/HomeFinance.Infra/"]
COPY ["src/HomeFinance.Models/HomeFinance.Domain.csproj", "src/HomeFinance.Models/"]
RUN dotnet restore "./src/HomeFinance.Api/HomeFinance.Api.csproj"
COPY . .
WORKDIR "/src/src/HomeFinance.Api"
RUN dotnet build "./HomeFinance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HomeFinance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeFinance.Api.dll"]
