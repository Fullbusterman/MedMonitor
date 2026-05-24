# ============================================================
# Dockerfile — Система мониторинга пациентов (MedMonitor)
# Multi-stage build: build → runtime
# ============================================================

# Этап 1: Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости (кэш слоя)
COPY src/MedMonitor/MedMonitor.csproj ./MedMonitor/
RUN dotnet restore ./MedMonitor/MedMonitor.csproj

# Копируем весь исходный код и выполняем публикацию
COPY src/MedMonitor/ ./MedMonitor/
RUN dotnet publish ./MedMonitor/MedMonitor.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Этап 2: Минимальный runtime-образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Создаём директорию для SQLite БД с правильными правами
RUN mkdir -p /app/data && chmod 777 /app/data

# Копируем собранное приложение из этапа сборки
COPY --from=build /app/publish .

# Переменные окружения
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/medmonitor.db"

# Порт приложения
EXPOSE 8080

# Точка входа
ENTRYPOINT ["dotnet", "MedMonitor.dll"]
