# Use the official .NET image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY NpvCalculator.Api/*.csproj ./NpvCalculator.Api/
RUN dotnet restore ./NpvCalculator.Api/NpvCalculator.Api.csproj

# Copy everything else and build
COPY . ./
WORKDIR /app/NpvCalculator.Api
RUN dotnet build NpvCalculator.Api.csproj -c Release -o /app/build

# Publish the application
FROM build-env AS publish
RUN dotnet publish NpvCalculator.Api.csproj -c Release -o /app/publish

# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NpvCalculator.Api.dll"]
