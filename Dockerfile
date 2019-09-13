# This only containerizes the backend component, as the fronend is already hosted on Amazon S3
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /backend

# Copy csproj and restore as distinct layers
# COPY *.csproj ./

# Copy everything else and build
COPY ./backend ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /backend
COPY --from=build-env /backend/out .
ENTRYPOINT ["dotnet", "PharmacyBackend.dll"]

