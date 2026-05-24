using Microsoft.EntityFrameworkCore;
using MedMonitor.Data;
using MedMonitor.Models;

namespace MedMonitor.Services;

/// <summary>Сервис для работы с родственниками пациентов</summary>
public interface IRelativeService
{
    Task<List<Relative>> GetRelativesByPatientIdAsync(int patientId);
    Task<Relative> CreateRelativeAsync(Relative relative);
    Task<Relative> UpdateRelativeAsync(Relative relative);
    Task DeleteRelativeAsync(int id);
}

public class RelativeService : IRelativeService
{
    private readonly MedMonitorDbContext _context;
    public RelativeService(MedMonitorDbContext context) => _context = context;

    public async Task<List<Relative>> GetRelativesByPatientIdAsync(int patientId)
        => await _context.Relatives
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.IsPrimaryContact)
            .ToListAsync();

    public async Task<Relative> CreateRelativeAsync(Relative relative)
    {
        _context.Relatives.Add(relative);
        await _context.SaveChangesAsync();
        return relative;
    }

    public async Task<Relative> UpdateRelativeAsync(Relative relative)
    {
        _context.Relatives.Update(relative);
        await _context.SaveChangesAsync();
        return relative;
    }

    public async Task DeleteRelativeAsync(int id)
    {
        var rel = await _context.Relatives.FindAsync(id);
        if (rel != null) { _context.Relatives.Remove(rel); await _context.SaveChangesAsync(); }
    }
}

/// <summary>Сервис для работы с показателями жизнедеятельности</summary>
public interface IVitalSignService
{
    Task<List<VitalSign>> GetVitalSignsByPatientIdAsync(int patientId, int limit = 20);
    Task<VitalSign> AddVitalSignAsync(VitalSign vitalSign);
    Task DeleteVitalSignAsync(int id);
}

public class VitalSignService : IVitalSignService
{
    private readonly MedMonitorDbContext _context;
    public VitalSignService(MedMonitorDbContext context) => _context = context;

    public async Task<List<VitalSign>> GetVitalSignsByPatientIdAsync(int patientId, int limit = 20)
        => await _context.VitalSigns
            .Where(v => v.PatientId == patientId)
            .OrderByDescending(v => v.MeasuredAt)
            .Take(limit)
            .ToListAsync();

    public async Task<VitalSign> AddVitalSignAsync(VitalSign vitalSign)
    {
        _context.VitalSigns.Add(vitalSign);
        await _context.SaveChangesAsync();
        return vitalSign;
    }

    public async Task DeleteVitalSignAsync(int id)
    {
        var v = await _context.VitalSigns.FindAsync(id);
        if (v != null) { _context.VitalSigns.Remove(v); await _context.SaveChangesAsync(); }
    }
}
