FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hackathon.Api.csproj", "Hackathon.Api/"]
RUN dotnet restore "Hackathon.Api/Hackathon.Api.csproj"
COPY . "HackathonApi/"
WORKDIR "/src/Hackathon.Api"
RUN dotnet build "Hackathon.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hackathon.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hackathon.Api.dll"]
