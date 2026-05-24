using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedMonitor.Models;

/// <summary>
/// Модель ближайшего родственника пациента
/// </summary>
public class Relative
{
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "Фамилия обязательна")]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя обязательно")]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Степень родства обязательна")]
    [MaxLength(50)]
    public string Relationship { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон обязателен")]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(150)]
    [EmailAddress]
    public string? Email { get; set; }

    public bool IsPrimaryContact { get; set; }

    // Навигационное свойство
    [ForeignKey(nameof(PatientId))]
    public Patient? Patient { get; set; }

    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}
