# Use the official .NET image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY NpvCalculator.Api/*.csproj ./NpvCalculator.Api/
COPY *.sln ./
RUN ls -la ./NpvCalculator.Api # List contents to ensure the directory exists
RUN dotnet restore ./NpvCalculator.Api/NpvCalculator.Api.csproj

# Copy everything else and build
COPY . ./
WORKDIR /src/NpvCalculator.Api
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "NpvCalculator.Api.dll"]
