# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["futArabicom/futArabicom.csproj", "futArabicom/"]
RUN dotnet restore "futArabicom/futArabicom.csproj"

# Copy the remaining source code and build the application
COPY . .
WORKDIR "/src/futArabicom"
RUN dotnet build "futArabicom.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "futArabicom.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

# Copy the published application
COPY --from=publish /app/publish .

# Copy the SQLite database file into the container
COPY ["futArabicom/database.db", "."]

# Set the entry point for the application
ENTRYPOINT ["dotnet", "futArabicom.dll"]
