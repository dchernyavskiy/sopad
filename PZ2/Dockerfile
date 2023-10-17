FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PZ2/PZ2.csproj", "PZ2/"]
RUN dotnet restore "PZ2/PZ2.csproj"
COPY . .
WORKDIR "/src/PZ2"
RUN dotnet build "PZ2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PZ2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PZ2.dll"]
