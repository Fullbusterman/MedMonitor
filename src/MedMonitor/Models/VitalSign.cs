using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedMonitor.Models;

/// <summary>
/// Модель показателей жизнедеятельности пациента (мониторинг)
/// </summary>
public class VitalSign
{
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }

    public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;

    /// <summary>Систолическое артериальное давление (мм рт.ст.)</summary>
    public int? BloodPressureSystolic { get; set; }

    /// <summary>Диастолическое артериальное давление (мм рт.ст.)</summary>
    public int? BloodPressureDiastolic { get; set; }

    /// <summary>Пульс (уд/мин)</summary>
    public int? HeartRate { get; set; }

    /// <summary>Температура тела (°C)</summary>
    public decimal? Temperature { get; set; }

    /// <summary>Насыщение крови кислородом (%)</summary>
    public int? OxygenSaturation { get; set; }

    /// <summary>Частота дыхания (вд/мин)</summary>
    public int? RespiratoryRate { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    [MaxLength(100)]
    public string? RecordedBy { get; set; }

    // Навигационное свойство
    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }

    public string BloodPressureFormatted =>
        BloodPressureSystolic.HasValue && BloodPressureDiastolic.HasValue
            ? $"{BloodPressureSystolic}/{BloodPressureDiastolic}"
            : "—";
}
