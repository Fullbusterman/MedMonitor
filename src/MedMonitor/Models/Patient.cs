using System.ComponentModel.DataAnnotations;

namespace MedMonitor.Models;

/// <summary>
/// Модель пациента медицинского учреждения
/// </summary>
public class Patient
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Фамилия обязательна")]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно")]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Пол обязателен")]
    [MaxLength(10)]
    public string Gender { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? PolicyNumber { get; set; }

    [MaxLength(500)]
    public string? Diagnoses { get; set; }

    public string? BloodType { get; set; }

    public bool HasAllergies { get; set; }

    [MaxLength(500)]
    public string? AllergyDescription { get; set; }

    public DateTime AdmissionDate { get; set; } = DateTime.UtcNow;

    public DateTime? DischargeDate { get; set; }

    [MaxLength(50)]
    public string Status { get; set; } = "Активный";

    // Навигационные свойства
    public ICollection<Relative> Relatives { get; set; } = new List<Relative>();
    public ICollection<VitalSign> VitalSigns { get; set; } = new List<VitalSign>();

    // Вычисляемые свойства
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    public int Age => (int)((DateTime.Today - DateOfBirth).TotalDays / 365.25);
}
