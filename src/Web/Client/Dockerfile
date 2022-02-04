#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Web/Client/Web.Client.csproj", "src/Web/Client/"]
COPY ["src/Web/Shared/Web.Shared.csproj", "src/Web/Shared/"]
RUN dotnet restore "src/Web/Client/Web.Client.csproj"
COPY . .
WORKDIR "/src/src/Web/Client"
RUN dotnet build "Web.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Client.dll"]
