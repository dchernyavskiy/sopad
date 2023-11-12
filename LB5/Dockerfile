FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LB5/LB5.csproj", "LB5/"]
RUN dotnet restore "LB5/LB5.csproj"
COPY . .
WORKDIR "/src/LB5"
RUN dotnet build "LB5.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LB5.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LB5.dll"]
