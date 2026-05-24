FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/MedMonitor/MedMonitor.csproj ./MedMonitor/
RUN dotnet restore ./MedMonitor/MedMonitor.csproj

COPY src/MedMonitor/ ./MedMonitor/
RUN dotnet publish ./MedMonitor/MedMonitor.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN mkdir -p /app/data && chmod 777 /app/data

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/medmonitor.db"

EXPOSE 8080

ENTRYPOINT ["dotnet", "MedMonitor.dll"]
