FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SoPaD/SoPaD.csproj", "SoPaD/"]
RUN dotnet restore "SoPaD/SoPaD.csproj"
COPY . .
WORKDIR "/src/SoPaD"
RUN dotnet build "SoPaD.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SoPaD.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SoPaD.dll"]
