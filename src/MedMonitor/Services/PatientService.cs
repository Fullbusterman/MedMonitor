using Microsoft.EntityFrameworkCore;
using MedMonitor.Data;
using MedMonitor.Models;

namespace MedMonitor.Services;

/// <summary>
/// Интерфейс сервиса для работы с пациентами
/// </summary>
public interface IPatientService
{
    Task<List<Patient>> GetAllPatientsAsync(string? searchTerm = null, string? statusFilter = null);
    Task<Patient?> GetPatientByIdAsync(int id);
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(int id);
    Task<int> GetTotalCountAsync();
    Task<int> GetActiveCountAsync();
}

/// <summary>
/// Сервис для работы с пациентами через EF Core
/// </summary>
public class PatientService : IPatientService
{
    private readonly MedMonitorDbContext _context;

    public PatientService(MedMonitorDbContext context)
    {
        _context = context;
    }

    /// <summary>Получить всех пациентов с фильтрацией и поиском</summary>
    public async Task<List<Patient>> GetAllPatientsAsync(string? searchTerm = null, string? statusFilter = null)
    {
        var query = _context.Patients
            .Include(p => p.Relatives)
            .Include(p => p.VitalSigns)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.ToLower();
            query = query.Where(p =>
                p.LastName.ToLower().Contains(term) ||
                p.FirstName.ToLower().Contains(term) ||
                (p.PolicyNumber != null && p.PolicyNumber.ToLower().Contains(term)));
        }

        if (!string.IsNullOrWhiteSpace(statusFilter) && statusFilter != "Все")
        {
            query = query.Where(p => p.Status == statusFilter);
        }

        return await query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToListAsync();
    }

    /// <summary>Получить пациента по идентификатору</summary>
    public async Task<Patient?> GetPatientByIdAsync(int id)
    {
        return await _context.Patients
            .Include(p => p.Relatives)
            .Include(p => p.VitalSigns.OrderByDescending(v => v.MeasuredAt))
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>Создать нового пациента</summary>
    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    /// <summary>Обновить данные пациента</summary>
    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    /// <summary>Удалить пациента (каскадно удаляются родственники и показатели)</summary>
    public async Task DeletePatientAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetTotalCountAsync() => await _context.Patients.CountAsync();
    public async Task<int> GetActiveCountAsync() => await _context.Patients.CountAsync(p => p.Status == "Активный");
}
