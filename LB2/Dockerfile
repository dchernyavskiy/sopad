FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LB2/LB2.csproj", "LB2/"]
RUN dotnet restore "LB2/LB2.csproj"
COPY . .
WORKDIR "/src/LB2"
RUN dotnet build "LB2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LB2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LB2.dll"]
