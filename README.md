# 🏥 MedMonitor — Система мониторинга пациентов

[![Docker Hub](https://img.shields.io/badge/Docker%20Hub-medmonitor-2496ED?logo=docker&logoColor=white)](https://hub.docker.com/r/fullbusterman/medmonitor)
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


## ⚙️ Переменные окружения

| Переменная | По умолчанию | Описание |
|-----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | `Production` | Среда |
| `ASPNETCORE_URLS` | `http://+:8080` | Порт |
| `ConnectionStrings__DefaultConnection` | `Data Source=/app/data/medmonitor.db` | SQLite путь |

---

## 🔗 Ссылки

- **GitHub:** https://github.com/fullbusterman/medmonitor  
- **Docker Hub:** https://hub.docker.com/r/fullbusterman/medmonitor
