# Use .NET SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY ./*.csproj ./
RUN dotnet restore

# Copy the rest of the application code and build
COPY . .
RUN dotnet publish -c Release -o /publish

# Use .NET runtime image for final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /publish .

# Copy Ocelot configuration file
COPY ocelot.json /app/ocelot.json

# Run the gateway
ENTRYPOINT ["dotnet", "APIGateway.dll"]
