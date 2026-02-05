# -----------------------------------------------------------------------
# STAGE 1: Base Runtime Environment
# -----------------------------------------------------------------------
# Using the optimized ASP.NET Core runtime image for the final execution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
# Expose standard ports for web traffic
EXPOSE 8080
EXPOSE 8081

# -----------------------------------------------------------------------
# STAGE 2: Build Environment
# -----------------------------------------------------------------------
# Using the full .NET SDK image to compile the source code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files and restore dependencies separately to leverage Docker caching
COPY ["EES.API/EES.API.csproj", "EES.API/"]
COPY ["EES.Application/EES.Application.csproj", "EES.Application/"]
COPY ["EES.Domain/EES.Domain.csproj", "EES.Domain/"]
COPY ["EES.Infrastructure/EES.Infrastructure.csproj", "EES.Infrastructure/"]

# Perform a restore of the NuGet packages
RUN dotnet restore "EES.API/EES.API.csproj"

# Copy the remaining source files and build the application
COPY . .
WORKDIR "/src/EES.API"
RUN dotnet build "EES.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# -----------------------------------------------------------------------
# STAGE 3: Publish
# -----------------------------------------------------------------------
# Prepare the binaries for deployment by stripping unnecessary debug files
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EES.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# -----------------------------------------------------------------------
# STAGE 4: Final Production Image
# -----------------------------------------------------------------------
# Copy only the published output from the publish stage to the base runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Define the entry point for the container execution
ENTRYPOINT ["dotnet", "EES.API.dll"]