FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore NetCoreAPI-Mongodb.sln

RUN dotnet build NetCoreAPI-Mongodb.sln -c Release --no-restore

RUN dotnet publish NetCoreAPI-Mongodb.sln -c Release -o /app/publish --no-build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "NetCoreAPI-Mongodb.dll"]