FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MyApi/MyApi.csproj MyApi/
RUN dotnet restore MyApi/MyApi.csproj

COPY . .
RUN dotnet publish MyApi/MyApi.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MyApi.dll"]
