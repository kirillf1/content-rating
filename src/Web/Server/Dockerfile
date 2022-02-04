#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/Server/Web.Server.csproj", "src/Web/Server/"]
COPY ["src/Web/Shared/Web.Shared.csproj", "src/Web/Shared/"]
COPY ["src/Web/Client/Web.Client.csproj", "src/Web/Client/"]
RUN dotnet restore "src/Web/Server/Web.Server.csproj"
COPY . .
WORKDIR "/src/src/Web/Server"
RUN dotnet build "Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Server.dll"]