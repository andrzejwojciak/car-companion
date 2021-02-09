FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY CarCompanion.Api/*.csproj ./CarCompanion.Api/
COPY CarCompanion.Shared/*.csproj ./CarCompanion.Shared/
RUN dotnet restore CarCompanion.Api/.

# Copy everything else and build
COPY CarCompanion.Api/. ./CarCompanion.Api/.
COPY CarCompanion.Shared/. ./CarCompanion.Shared/.
RUN dotnet publish CarCompanion.Api/*.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./CarCompanion.Api"]