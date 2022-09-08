#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tartak.WebApp/Server/Tartak.WebApp.Server.csproj", "Tartak.WebApp/Server/"]
COPY ["Tartak.WebApp.Library/Tartak.WebApp.Library.csproj", "Tartak.WebApp.Library/"]
COPY ["Tartak.WebApp/Shared/Tartak.WebApp.Shared.csproj", "Tartak.WebApp/Shared/"]
COPY ["Tartak.WebApp/Client/Tartak.WebApp.Client.csproj", "Tartak.WebApp/Client/"]
RUN dotnet restore "Tartak.WebApp/Server/Tartak.WebApp.Server.csproj"
COPY . .
WORKDIR "/src/Tartak.WebApp/Server"
RUN dotnet build "Tartak.WebApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tartak.WebApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tartak.WebApp.Server.dll"]