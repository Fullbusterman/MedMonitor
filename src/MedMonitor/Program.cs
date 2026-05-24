using Microsoft.EntityFrameworkCore;
using MedMonitor.Components;
using MedMonitor.Data;
using MedMonitor.Services;

var builder = WebApplication.CreateBuilder(args);

// Razor Components + Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core — SQLite
builder.Services.AddDbContext<MedMonitorDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=/app/data/medmonitor.db"));

// Регистрация сервисов
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IRelativeService, RelativeService>();
builder.Services.AddScoped<IVitalSignService, VitalSignService>();

var app = builder.Build();

// Применение миграций при старте (один раз)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MedMonitorDbContext>();
    db.Database.Migrate();
}

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Автоматическое создание/миграция БД и заполнение данными
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Замените AppDbContext на имя вашего класса контекста базы данных!
        var context = services.GetRequiredService<MedMonitor.Data.MedMonitorDbContext>(); 
        
        // Автоматически создает файл базы данных и применяет миграции, если их нет
        context.Database.EnsureCreated();; 
        
        // Заполняем базу нашими 8 пациентами
        MedMonitor.Data.DbInitializer.Seed(context); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при инициализации или миграции базы данных.");
    }
}

app.Run();
