using Microsoft.EntityFrameworkCore;
using MedMonitor.Models;

namespace MedMonitor.Data;

/// <summary>
/// Контекст базы данных системы мониторинга пациентов
/// </summary>
public class MedMonitorDbContext : DbContext
{
    public MedMonitorDbContext(DbContextOptions<MedMonitorDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Relative> Relatives => Set<Relative>();
    public DbSet<VitalSign> VitalSigns => Set<VitalSign>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация Patient
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Status).HasDefaultValue("Активный");
            entity.HasIndex(p => p.PolicyNumber).IsUnique().HasFilter("[PolicyNumber] IS NOT NULL");
        });

        // Конфигурация Relative: 1:N с Patient
        modelBuilder.Entity<Relative>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.HasOne(r => r.Patient)
                  .WithMany(p => p.Relatives)
                  .HasForeignKey(r => r.PatientId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Конфигурация VitalSign: 1:N с Patient
        modelBuilder.Entity<VitalSign>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.HasOne(v => v.Patient)
                  .WithMany(p => p.VitalSigns)
                  .HasForeignKey(v => v.PatientId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed-данные: пациенты
        modelBuilder.Entity<Patient>().HasData(
            new Patient
            {
                Id = 1, LastName = "Иванов", FirstName = "Алексей", MiddleName = "Петрович",
                DateOfBirth = new DateTime(1975, 3, 14), Gender = "М",
                PhoneNumber = "+7-495-123-4501", Address = "г. Москва, ул. Ленина, д. 10, кв. 5",
                PolicyNumber = "ОМС-001-2023", BloodType = "II+",
                Diagnoses = "Гипертоническая болезнь II ст.", HasAllergies = false,
                AdmissionDate = new DateTime(2024, 1, 15), Status = "Активный"
            },
            new Patient
            {
                Id = 2, LastName = "Петрова", FirstName = "Мария", MiddleName = "Сергеевна",
                DateOfBirth = new DateTime(1988, 7, 22), Gender = "Ж",
                PhoneNumber = "+7-495-123-4502", Address = "г. Москва, ул. Мира, д. 45, кв. 12",
                PolicyNumber = "ОМС-002-2023", BloodType = "I+",
                Diagnoses = "Сахарный диабет 2 типа", HasAllergies = true,
                AllergyDescription = "Пенициллин",
                AdmissionDate = new DateTime(2024, 2, 3), Status = "Активный"
            },
            new Patient
            {
                Id = 3, LastName = "Сидоров", FirstName = "Николай", MiddleName = "Иванович",
                DateOfBirth = new DateTime(1960, 11, 5), Gender = "М",
                PhoneNumber = "+7-495-123-4503", Address = "г. Москва, пр. Победы, д. 7",
                PolicyNumber = "ОМС-003-2023", BloodType = "III-",
                Diagnoses = "ИБС, стабильная стенокардия", HasAllergies = true,
                AllergyDescription = "Аспирин, ибупрофен",
                AdmissionDate = new DateTime(2024, 3, 10), Status = "Активный"
            },
            new Patient
            {
                Id = 4, LastName = "Кузнецова", FirstName = "Ольга", MiddleName = "Андреевна",
                DateOfBirth = new DateTime(1995, 6, 18), Gender = "Ж",
                PhoneNumber = "+7-495-123-4504", Address = "г. Москва, ул. Садовая, д. 3, кв. 8",
                PolicyNumber = "ОМС-004-2023", BloodType = "IV+",
                Diagnoses = "Бронхиальная астма лёгкой степени", HasAllergies = false,
                AdmissionDate = new DateTime(2024, 4, 20), Status = "Выписан",
                DischargeDate = new DateTime(2024, 5, 5)
            },
            new Patient
            {
                Id = 5, LastName = "Морозов", FirstName = "Дмитрий", MiddleName = "Викторович",
                DateOfBirth = new DateTime(1982, 9, 30), Gender = "М",
                PhoneNumber = "+7-495-123-4505", Address = "г. Москва, ул. Новая, д. 21",
                PolicyNumber = "ОМС-005-2023", BloodType = "II-",
                Diagnoses = "Остеохондроз шейного отдела позвоночника", HasAllergies = false,
                AdmissionDate = new DateTime(2024, 5, 12), Status = "Активный"
            }
        );

        // Seed-данные: родственники
        modelBuilder.Entity<Relative>().HasData(
            new Relative { Id = 1, PatientId = 1, LastName = "Иванова", FirstName = "Наталья", MiddleName = "Викторовна", Relationship = "Супруга", PhoneNumber = "+7-495-200-0101", IsPrimaryContact = true, Email = "ivanova.n@mail.ru" },
            new Relative { Id = 2, PatientId = 1, LastName = "Иванов", FirstName = "Пётр", MiddleName = "Алексеевич", Relationship = "Сын", PhoneNumber = "+7-495-200-0102", IsPrimaryContact = false },
            new Relative { Id = 3, PatientId = 2, LastName = "Петров", FirstName = "Сергей", MiddleName = "Михайлович", Relationship = "Отец", PhoneNumber = "+7-495-200-0201", IsPrimaryContact = true, Email = "petrov.s@yandex.ru" },
            new Relative { Id = 4, PatientId = 2, LastName = "Петрова", FirstName = "Ирина", MiddleName = "Николаевна", Relationship = "Мать", PhoneNumber = "+7-495-200-0202", IsPrimaryContact = false },
            new Relative { Id = 5, PatientId = 3, LastName = "Сидорова", FirstName = "Татьяна", MiddleName = "Александровна", Relationship = "Супруга", PhoneNumber = "+7-495-200-0301", IsPrimaryContact = true },
            new Relative { Id = 6, PatientId = 4, LastName = "Кузнецов", FirstName = "Андрей", MiddleName = "Павлович", Relationship = "Отец", PhoneNumber = "+7-495-200-0401", IsPrimaryContact = true, Email = "kuznetsov.a@gmail.com" },
            new Relative { Id = 7, PatientId = 5, LastName = "Морозова", FirstName = "Елена", MiddleName = "Дмитриевна", Relationship = "Супруга", PhoneNumber = "+7-495-200-0501", IsPrimaryContact = true }
        );

        // Seed-данные: показатели жизнедеятельности
        modelBuilder.Entity<VitalSign>().HasData(
            new VitalSign { Id = 1, PatientId = 1, MeasuredAt = new DateTime(2024, 6, 1, 8, 0, 0), BloodPressureSystolic = 150, BloodPressureDiastolic = 95, HeartRate = 82, Temperature = 36.6m, OxygenSaturation = 97, RespiratoryRate = 18, RecordedBy = "Медсестра Смирнова А.В." },
            new VitalSign { Id = 2, PatientId = 1, MeasuredAt = new DateTime(2024, 6, 1, 14, 0, 0), BloodPressureSystolic = 145, BloodPressureDiastolic = 90, HeartRate = 78, Temperature = 36.7m, OxygenSaturation = 98, RespiratoryRate = 17, RecordedBy = "Медсестра Смирнова А.В." },
            new VitalSign { Id = 3, PatientId = 2, MeasuredAt = new DateTime(2024, 6, 1, 8, 0, 0), BloodPressureSystolic = 125, BloodPressureDiastolic = 80, HeartRate = 76, Temperature = 36.5m, OxygenSaturation = 99, RespiratoryRate = 16, RecordedBy = "Медсестра Козлова Л.Н." },
            new VitalSign { Id = 4, PatientId = 3, MeasuredAt = new DateTime(2024, 6, 1, 8, 0, 0), BloodPressureSystolic = 135, BloodPressureDiastolic = 85, HeartRate = 68, Temperature = 36.8m, OxygenSaturation = 96, RespiratoryRate = 19, RecordedBy = "Медсестра Смирнова А.В.", Notes = "Жалобы на боли в груди" },
            new VitalSign { Id = 5, PatientId = 5, MeasuredAt = new DateTime(2024, 6, 1, 8, 0, 0), BloodPressureSystolic = 120, BloodPressureDiastolic = 78, HeartRate = 72, Temperature = 36.6m, OxygenSaturation = 98, RespiratoryRate = 16, RecordedBy = "Медсестра Козлова Л.Н." }
        );
    }
}
