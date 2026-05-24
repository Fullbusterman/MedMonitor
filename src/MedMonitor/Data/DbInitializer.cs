using MedMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedMonitor.Data; // Укажите ваш namespace (например, MedMonitor.Data)

public static class DbInitializer
{
    public static void Seed(DbContext context)
    {
        // Проверяем, есть ли уже пациенты в БД. Если есть — ничего не делаем
        if (context.Set<Patient>().Any())
        {
            return; 
        }

        // 1. Создаем пациентов
        var patients = new List<Patient>
        {
            new Patient { Id = 1, LastName = "Иванов", FirstName = "Петр", MiddleName = "Сергеевич", DateOfBirth = new DateTime(1985, 5, 15), Gender = "Мужской", PhoneNumber = "+7 (999) 111-22-33", Address = "г. Москва, ул. Ленина, д. 10, кв. 5", PolicyNumber = "1111222233334444", Diagnoses = "Гипертоническая болезнь 2 степени", BloodType = "A_Plus", HasAllergies = false, Status = "Активный" },
            new Patient { Id = 2, LastName = "Петрова", FirstName = "Анна", MiddleName = "Васильевна", DateOfBirth = new DateTime(1993, 10, 22), Gender = "Женский", PhoneNumber = "+7 (999) 444-55-66", Address = "г. Санкт-Петербург, пр. Славы, д. 5, кв. 12", PolicyNumber = "5555666677778888", Diagnoses = "Острая респираторная вирусная инфекция, острый бронхит", BloodType = "0_Plus", HasAllergies = true, AllergyDescription = "Аллергия на пенициллин (сыпь)", Status = "Активный" },
            new Patient { Id = 3, LastName = "Сидоров", FirstName = "Илья", MiddleName = "Игоревич", DateOfBirth = new DateTime(1960, 2, 3), Gender = "Мужской", PhoneNumber = "+7 (999) 777-88-99", Address = "г. Новосибирск, ул. Кирова, д. 44", PolicyNumber = "9999888877776666", Diagnoses = "Сахарный диабет 2 типа", BloodType = "B_Minus", HasAllergies = false, Status = "Выписан", DischargeDate = DateTime.UtcNow.AddDays(-2) },
            new Patient { Id = 4, LastName = "Кузнецова", FirstName = "Елена", MiddleName = "Николаевна", DateOfBirth = new DateTime(1952, 8, 14), Gender = "Женский", PhoneNumber = "+7 (900) 123-45-67", Address = "г. Екатеринбург, ул. Малышева, д. 15, кв. 89", PolicyNumber = "4444555566667777", Diagnoses = "ИБС. Постинфарктный кардиосклероз. ХСН 2А.", BloodType = "AB_Plus", HasAllergies = true, AllergyDescription = "Отек Квинке на новокаин", Status = "Реанимация" },
            new Patient { Id = 5, LastName = "Смирнов", FirstName = "Максим", MiddleName = "Дмитриевич", DateOfBirth = new DateTime(2015, 3, 10), Gender = "Мужской", PhoneNumber = "+7 (911) 987-65-43", Address = "г. Нижний Новгород, ул. Родионова, д. 4, кв. 102", PolicyNumber = "8888777766665555", Diagnoses = "Острый аппендицит, состояние после аппендэктомии", BloodType = "0_Plus", HasAllergies = false, Status = "Активный" },
            new Patient { Id = 6, LastName = "Васильева", FirstName = "Ольга", MiddleName = "Олеговна", DateOfBirth = new DateTime(1999, 11, 5), Gender = "Женский", PhoneNumber = "+7 (950) 555-44-33", Address = "г. Казань, ул. Баумана, д. 22, кв. 14", PolicyNumber = "2222333344445555", Diagnoses = "Беременность 22 недели. Железодефицитная анемия 1 степени.", BloodType = "A_Minus", HasAllergies = false, Status = "Активный" },
            new Patient { Id = 7, LastName = "Морозов", FirstName = "Артем", MiddleName = "Андреевич", DateOfBirth = new DateTime(1981, 1, 30), Gender = "Мужской", PhoneNumber = "+7 (960) 222-33-44", Address = "г. Самара, ул. Ново-Садовая, д. 108, кв. 5", PolicyNumber = "7777666655554444", Diagnoses = "Закрытый перелом обеих костей левой голени со смещением", BloodType = "B_Plus", HasAllergies = true, AllergyDescription = "Крапивница на анальгин", Status = "Выписан", DischargeDate = DateTime.UtcNow.AddDays(-1) },
            new Patient { Id = 8, LastName = "Федорова", FirstName = "Дарья", MiddleName = "Сергеевна", DateOfBirth = new DateTime(2007, 7, 17), Gender = "Женский", PhoneNumber = "+7 (999) 888-11-22", Address = "г. Челябинск, пр. Ленина, д. 50, кв. 7", PolicyNumber = "3333444455556666", Diagnoses = "Острый поверхностный гастрит, обострение", BloodType = "A_Plus", HasAllergies = false, Status = "Активный" }
        };

        context.Set<Patient>().AddRange(patients);
        context.SaveChanges();

        // 2. Создаем родственников (привязка по PatientId)
        var relatives = new List<Relative>
        {
            new Relative { PatientId = 1, LastName = "Иванова", FirstName = "Мария", MiddleName = "Ивановна", Relationship = "Жена", PhoneNumber = "+7 (999) 111-22-34", Address = "г. Москва, ул. Ленина, д. 10, кв. 5", Email = "m.ivanova@example.com", IsPrimaryContact = true },
            new Relative { PatientId = 2, LastName = "Петров", FirstName = "Василий", MiddleName = "Петрович", Relationship = "Отец", PhoneNumber = "+7 (999) 444-55-00", Address = "г. Самара, ул. Полевая, д. 12", IsPrimaryContact = true },
            new Relative { PatientId = 4, LastName = "Кузнецов", FirstName = "Олег", MiddleName = "Игоревич", Relationship = "Сын", PhoneNumber = "+7 (900) 123-00-11", Address = "г. Екатеринбург, ул. Малышева, д. 15, кв. 89", Email = "oleg.kuzn@example.com", IsPrimaryContact = true },
            new Relative { PatientId = 4, LastName = "Кузнецова", FirstName = "Анна", MiddleName = "Олеговна", Relationship = "Внучка", PhoneNumber = "+7 (900) 123-00-22", Address = "г. Екатеринбург, ул. Радищева, д. 2, кв. 14", IsPrimaryContact = false },
            new Relative { PatientId = 5, LastName = "Смирнов", FirstName = "Дмитрий", MiddleName = "Александрович", Relationship = "Отец", PhoneNumber = "+7 (911) 987-00-01", Address = "г. Нижний Новгород, ул. Родионова, д. 4, кв. 102", Email = "dmitry.sm@example.com", IsPrimaryContact = true },
            new Relative { PatientId = 5, LastName = "Смирнова", FirstName = "Елена", MiddleName = "Викторовна", Relationship = "Мать", PhoneNumber = "+7 (911) 987-00-02", Address = "г. Нижний Новгород, ул. Родионова, д. 4, кв. 102", IsPrimaryContact = false },
            new Relative { PatientId = 7, LastName = "Морозова", FirstName = "Светлана", MiddleName = "Юрьевна", Relationship = "Жена", PhoneNumber = "+7 (960) 222-00-11", Address = "г. Самара, ул. Ново-Садовая, д. 108, кв. 5", Email = "sveta_morozova@example.com", IsPrimaryContact = true }
        };

        context.Set<Relative>().AddRange(relatives);

        // 3. Создаем показатели жизнедеятельности
        var vitals = new List<VitalSign>
        {
            new VitalSign { PatientId = 1, BloodPressureSystolic = 145, BloodPressureDiastolic = 95, HeartRate = 82, Temperature = 36.6m, OxygenSaturation = 98, RespiratoryRate = 16, Notes = "Давление стабильно повышенное. Жалобы на легкую головную боль в затылке.", RecordedBy = "м/с Петрова И.В." },
            new VitalSign { PatientId = 2, BloodPressureSystolic = 110, BloodPressureDiastolic = 70, HeartRate = 90, Temperature = 38.2m, OxygenSaturation = 96, RespiratoryRate = 19, Notes = "Лихорадка. Дан жаропонижающий препарат (Парацетамол).", RecordedBy = "м/с Петрова И.В." },
            new VitalSign { PatientId = 2, BloodPressureSystolic = 115, BloodPressureDiastolic = 75, HeartRate = 80, Temperature = 37.3m, OxygenSaturation = 97, RespiratoryRate = 17, Notes = "Контроль после жаропонижающего. Температура снижается.", RecordedBy = "м/с Петрова И.В." },
            new VitalSign { PatientId = 4, BloodPressureSystolic = 90, BloodPressureDiastolic = 60, HeartRate = 115, Temperature = 36.4m, OxygenSaturation = 91, RespiratoryRate = 24, Notes = "Состояние тяжелое. Сатурация падает, подключен кислородный концентратор.", RecordedBy = "д-р Федоров А.А." },
            new VitalSign { PatientId = 5, BloodPressureSystolic = 115, BloodPressureDiastolic = 75, HeartRate = 76, Temperature = 37.1m, OxygenSaturation = 99, RespiratoryRate = 18, Notes = "Вторые сутки после аппендэктомии. Показатели в норме.", RecordedBy = "м/с Сидорова О.Н." },
            new VitalSign { PatientId = 6, BloodPressureSystolic = 115, BloodPressureDiastolic = 70, HeartRate = 78, Temperature = 36.5m, OxygenSaturation = 98, RespiratoryRate = 16, Notes = "Плановый утренний замер. Показатели без отклонений.", RecordedBy = "м/с Сидорова О.Н." },
            new VitalSign { PatientId = 8, BloodPressureSystolic = 120, BloodPressureDiastolic = 80, HeartRate = 85, Temperature = 36.8m, OxygenSaturation = 98, RespiratoryRate = 17, Notes = "Жалобы на острые боли в желудке натощак.", RecordedBy = "м/с Петрова И.В." }
        };

        context.Set<VitalSign>().AddRange(vitals);
        context.SaveChanges();
    }
}
