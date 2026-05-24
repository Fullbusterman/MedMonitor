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

app.Run();
