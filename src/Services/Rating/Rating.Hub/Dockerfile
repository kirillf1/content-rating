#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Rating/Rating.Hub/Rating.Hub.csproj", "src/Services/Rating/Rating.Hub/"]
COPY ["src/Services/Rating/Rating.Application/Rating.Application.csproj", "src/Services/Rating/Rating.Application/"]
COPY ["src/Services/Rating/Rating.Domain/Rating.Domain.csproj", "src/Services/Rating/Rating.Domain/"]
COPY ["src/Services/Rating/Rating.Infrastructure/Rating.Infrastructure.csproj", "src/Services/Rating/Rating.Infrastructure/"]
RUN dotnet restore "src/Services/Rating/Rating.Hub/Rating.Hub.csproj"
COPY . .
WORKDIR "/src/src/Services/Rating/Rating.Hub"
RUN dotnet build "Rating.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rating.Hub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rating.Hub.dll"]