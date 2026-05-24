using Microsoft.EntityFrameworkCore;
using MedMonitor.Components;
using MedMonitor.Data;
using MedMonitor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core — SQLite с подходом Code First
builder.Services.AddDbContext<MedMonitorDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=/app/data/medmonitor.db"));

// Регистрация сервисов через Dependency Injection
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IRelativeService, RelativeService>();
builder.Services.AddScoped<IVitalSignService, VitalSignService>();

var app = builder.Build();

// Применение миграций и заполнение БД при старте
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MedMonitorDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Блок автоматической инициализации базы данных при старте
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MedMonitor.Data.MedMonitorDbContext>();
        
        // Эта команда проверяет наличие БД. Если её нет, она создаёт файл 
        // и автоматически применяет все миграции вместе с вашими Seed-данными.
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Произошла ошибка при накате миграций на базу данных.");
    }
}
app.Run();
