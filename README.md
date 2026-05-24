# 🏥 MedMonitor — Система мониторинга пациентов

[![Docker Hub](https://img.shields.io/badge/Docker%20Hub-medmonitor-2496ED?logo=docker&logoColor=white)](https://hub.docker.com/r/youruser/medmonitor)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor)](https://blazor.net)
[![EF Core](https://img.shields.io/badge/EF%20Core-8.0-green)](https://learn.microsoft.com/ef/core)

Веб-приложение для мониторинга пациентов медицинского учреждения.  
Стек: **ASP.NET Core 8 + Blazor Server + Entity Framework Core + SQLite + Docker**.

---

## 📐 Архитектура

```
MedMonitor/
├── .github/workflows/
│   └── docker-publish.yml       # CI/CD → Docker Hub
├── src/MedMonitor/
│   ├── _Imports.razor            # Глобальные using-директивы
│   ├── MedMonitor.csproj
│   ├── Program.cs                # DI, Middleware, auto-migrate
│   ├── appsettings.json
│   ├── Components/
│   │   ├── App.razor             # HTML-оболочка (Bootstrap CDN)
│   │   ├── Routes.razor
│   │   ├── _Imports.razor
│   │   ├── Layout/
│   │   │   ├── MainLayout.razor
│   │   │   └── NavMenu.razor
│   │   └── Pages/
│   │       ├── Home.razor        # Дашборд со статистикой
│   │       ├── Patients.razor    # Список + поиск + фильтр
│   │       ├── PatientDetail.razor  # Карточка пациента
│   │       ├── PatientForm.razor    # Создание / редактирование
│   │       ├── RelativeForm.razor   # Родственник: создание / ред.
│   │       ├── Vitals.razor         # Мониторинг всех активных
│   │       └── Error.razor
│   ├── Data/
│   │   └── MedMonitorDbContext.cs   # DbContext + Seed data
│   ├── Models/
│   │   ├── Patient.cs
│   │   ├── Relative.cs
│   │   └── VitalSign.cs
│   ├── Services/
│   │   ├── PatientService.cs
│   │   └── RelativeVitalSignService.cs
│   ├── Migrations/
│   │   ├── 20240601000000_InitialCreate.cs
│   │   └── MedMonitorDbContextModelSnapshot.cs
│   └── wwwroot/
│       ├── css/app.css
│       ├── favicon.svg
│       └── MedMonitor.styles.css
├── Dockerfile                    # Multi-stage build
├── docker-compose.yml
├── .dockerignore
├── publish_to_dockerhub.sh       # Linux/macOS
└── publish_to_dockerhub.ps1      # Windows PowerShell
```

## 🗄️ Модель данных

```
Patient (1) ──── (N) Relative      каскадное удаление
Patient (1) ──── (N) VitalSign     каскадное удаление
```

| Сущность    | Ключевые поля |
|-------------|---------------|
| `Patient`   | Id, ФИО, DateOfBirth, Gender, PolicyNumber (уникальный), BloodType, Diagnoses, HasAllergies, Status |
| `Relative`  | Id, PatientId (FK), ФИО, Relationship, PhoneNumber, IsPrimaryContact |
| `VitalSign` | Id, PatientId (FK), MeasuredAt, BloodPressure, HeartRate, Temperature, OxygenSaturation, RespiratoryRate |

---

## 🚀 Запуск

### Вариант 1 — Docker Hub (одна команда)

```bash
docker run -d \
  -p 8080:8080 \
  -v medmonitor_data:/app/data \
  --name medmonitor \
  youruser/medmonitor:latest

# Открыть → http://localhost:8080
```

### Вариант 2 — docker-compose

```bash
git clone https://github.com/youruser/medmonitor.git
cd medmonitor

docker-compose up -d

# Открыть → http://localhost:8080

docker-compose down          # остановить
docker-compose down -v       # остановить + удалить данные
```

### Вариант 3 — локальная разработка (.NET 8 SDK)

```bash
cd src/MedMonitor
dotnet restore
dotnet run
# Открыть → http://localhost:5001
```

---

## 🐳 Публикация на Docker Hub

### Способ А — скрипт (рекомендуется)

**Linux / macOS:**
```bash
chmod +x publish_to_dockerhub.sh
./publish_to_dockerhub.sh johndoe 1.0.0
```

**Windows (PowerShell):**
```powershell
.\publish_to_dockerhub.ps1 -DockerUser johndoe -Version 1.0.0
```

### Способ Б — вручную

```bash
# 1. Авторизация
docker login

# 2. Сборка с тегами
docker build -t johndoe/medmonitor:latest -t johndoe/medmonitor:1.0.0 .

# 3. Публикация
docker push johndoe/medmonitor:latest
docker push johndoe/medmonitor:1.0.0
```

### Способ В — GitHub Actions (автоматически)

Добавьте секреты в репозиторий GitHub:
- `DOCKERHUB_USERNAME` — ваш логин Docker Hub  
- `DOCKERHUB_TOKEN`    — Access Token из [hub.docker.com/settings/security](https://hub.docker.com/settings/security)

Каждый push в `main` или тег `v*.*.*` автоматически собирает и публикует образ.

---

## ⚙️ Переменные окружения

| Переменная | По умолчанию | Описание |
|-----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | `Production` | Среда |
| `ASPNETCORE_URLS` | `http://+:8080` | Порт |
| `ConnectionStrings__DefaultConnection` | `Data Source=/app/data/medmonitor.db` | SQLite путь |

---

## 🔗 Ссылки

- **GitHub:** https://github.com/youruser/medmonitor  
- **Docker Hub:** https://hub.docker.com/r/youruser/medmonitor
