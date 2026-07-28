# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["ProductAPI/ProductAPI.csproj", "ProductAPI/"]
COPY ["ProductApplication/ProductApplication.csproj", "ProductApplication/"]
COPY ["ProductDomain/ProductDomain.csproj", "ProductDomain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj", "Services/"]

# Restore dependencies
RUN dotnet restore "ProductAPI/ProductAPI.csproj"

# Copy all source
COPY . .

WORKDIR "/src/ProductAPI"

# Publish
RUN dotnet publish "ProductAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ProductAPI.dll"]