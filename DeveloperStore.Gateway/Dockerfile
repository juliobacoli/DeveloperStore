FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DeveloperStore.Gateway/DeveloperStore.Gateway.csproj", "DeveloperStore.Gateway/"]
RUN dotnet restore "DeveloperStore.Gateway/DeveloperStore.Gateway.csproj"
COPY . .
WORKDIR "/src/DeveloperStore.Gateway"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveloperStore.Gateway.dll"]
