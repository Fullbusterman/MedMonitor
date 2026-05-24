using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MedMonitor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    PolicyNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Diagnoses = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    BloodType = table.Column<string>(type: "TEXT", nullable: true),
                    HasAllergies = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllergyDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AdmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "Активный")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relatives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Relationship = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    IsPrimaryContact = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relatives_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasuredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BloodPressureSystolic = table.Column<int>(type: "INTEGER", nullable: true),
                    BloodPressureDiastolic = table.Column<int>(type: "INTEGER", nullable: true),
                    HeartRate = table.Column<int>(type: "INTEGER", nullable: true),
                    Temperature = table.Column<decimal>(type: "TEXT", nullable: true),
                    OxygenSaturation = table.Column<int>(type: "INTEGER", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    RecordedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VitalSigns_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Seed patients
            migrationBuilder.InsertData("Patients", 
                new[] {"Id","LastName","FirstName","MiddleName","DateOfBirth","Gender","PhoneNumber","Address","PolicyNumber","Diagnoses","BloodType","HasAllergies","AllergyDescription","AdmissionDate","DischargeDate","Status"},
                new object[] {1,"Иванов","Алексей","Петрович","1975-03-14","М","+7-495-123-4501","г. Москва, ул. Ленина, д. 10, кв. 5","ОМС-001-2023","Гипертоническая болезнь II ст.","II+",false,null,"2024-01-15",null,"Активный"});
            migrationBuilder.InsertData("Patients",
                new[] {"Id","LastName","FirstName","MiddleName","DateOfBirth","Gender","PhoneNumber","Address","PolicyNumber","Diagnoses","BloodType","HasAllergies","AllergyDescription","AdmissionDate","DischargeDate","Status"},
                new object[] {2,"Петрова","Мария","Сергеевна","1988-07-22","Ж","+7-495-123-4502","г. Москва, ул. Мира, д. 45, кв. 12","ОМС-002-2023","Сахарный диабет 2 типа","I+",true,"Пенициллин","2024-02-03",null,"Активный"});
            migrationBuilder.InsertData("Patients",
                new[] {"Id","LastName","FirstName","MiddleName","DateOfBirth","Gender","PhoneNumber","Address","PolicyNumber","Diagnoses","BloodType","HasAllergies","AllergyDescription","AdmissionDate","DischargeDate","Status"},
                new object[] {3,"Сидоров","Николай","Иванович","1960-11-05","М","+7-495-123-4503","г. Москва, пр. Победы, д. 7","ОМС-003-2023","ИБС, стабильная стенокардия","III-",true,"Аспирин, ибупрофен","2024-03-10",null,"Активный"});
            migrationBuilder.InsertData("Patients",
                new[] {"Id","LastName","FirstName","MiddleName","DateOfBirth","Gender","PhoneNumber","Address","PolicyNumber","Diagnoses","BloodType","HasAllergies","AllergyDescription","AdmissionDate","DischargeDate","Status"},
                new object[] {4,"Кузнецова","Ольга","Андреевна","1995-06-18","Ж","+7-495-123-4504","г. Москва, ул. Садовая, д. 3, кв. 8","ОМС-004-2023","Бронхиальная астма лёгкой степени","IV+",false,null,"2024-04-20","2024-05-05","Выписан"});
            migrationBuilder.InsertData("Patients",
                new[] {"Id","LastName","FirstName","MiddleName","DateOfBirth","Gender","PhoneNumber","Address","PolicyNumber","Diagnoses","BloodType","HasAllergies","AllergyDescription","AdmissionDate","DischargeDate","Status"},
                new object[] {5,"Морозов","Дмитрий","Викторович","1982-09-30","М","+7-495-123-4505","г. Москва, ул. Новая, д. 21","ОМС-005-2023","Остеохондроз шейного отдела позвоночника","II-",false,null,"2024-05-12",null,"Активный"});

            // Seed relatives
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {1,1,"Иванова","Наталья","Викторовна","Супруга","+7-495-200-0101",null,"ivanova.n@mail.ru",true});
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {2,1,"Иванов","Пётр","Алексеевич","Сын","+7-495-200-0102",null,null,false});
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {3,2,"Петров","Сергей","Михайлович","Отец","+7-495-200-0201",null,"petrov.s@yandex.ru",true});
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {4,2,"Петрова","Ирина","Николаевна","Мать","+7-495-200-0202",null,null,false});
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {5,3,"Сидорова","Татьяна","Александровна","Супруга","+7-495-200-0301",null,null,true});
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {6,4,"Кузнецов","Андрей","Павлович","Отец","+7-495-200-0401",null,"kuznetsov.a@gmail.com",true});
            migrationBuilder.InsertData("Relatives",
                new[] {"Id","PatientId","LastName","FirstName","MiddleName","Relationship","PhoneNumber","Address","Email","IsPrimaryContact"},
                new object[] {7,5,"Морозова","Елена","Дмитриевна","Супруга","+7-495-200-0501",null,null,true});

            // Seed vital signs
            migrationBuilder.InsertData("VitalSigns",
                new[] {"Id","PatientId","MeasuredAt","BloodPressureSystolic","BloodPressureDiastolic","HeartRate","Temperature","OxygenSaturation","RespiratoryRate","Notes","RecordedBy"},
                new object[] {1,1,"2024-06-01 08:00:00",150,95,82,36.6,97,18,null,"Медсестра Смирнова А.В."});
            migrationBuilder.InsertData("VitalSigns",
                new[] {"Id","PatientId","MeasuredAt","BloodPressureSystolic","BloodPressureDiastolic","HeartRate","Temperature","OxygenSaturation","RespiratoryRate","Notes","RecordedBy"},
                new object[] {2,1,"2024-06-01 14:00:00",145,90,78,36.7,98,17,null,"Медсестра Смирнова А.В."});
            migrationBuilder.InsertData("VitalSigns",
                new[] {"Id","PatientId","MeasuredAt","BloodPressureSystolic","BloodPressureDiastolic","HeartRate","Temperature","OxygenSaturation","RespiratoryRate","Notes","RecordedBy"},
                new object[] {3,2,"2024-06-01 08:00:00",125,80,76,36.5,99,16,null,"Медсестра Козлова Л.Н."});
            migrationBuilder.InsertData("VitalSigns",
                new[] {"Id","PatientId","MeasuredAt","BloodPressureSystolic","BloodPressureDiastolic","HeartRate","Temperature","OxygenSaturation","RespiratoryRate","Notes","RecordedBy"},
                new object[] {4,3,"2024-06-01 08:00:00",135,85,68,36.8,96,19,"Жалобы на боли в груди","Медсестра Смирнова А.В."});
            migrationBuilder.InsertData("VitalSigns",
                new[] {"Id","PatientId","MeasuredAt","BloodPressureSystolic","BloodPressureDiastolic","HeartRate","Temperature","OxygenSaturation","RespiratoryRate","Notes","RecordedBy"},
                new object[] {5,5,"2024-06-01 08:00:00",120,78,72,36.6,98,16,null,"Медсестра Козлова Л.Н."});

            migrationBuilder.CreateIndex(
                name: "IX_Relatives_PatientId",
                table: "Relatives",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_PatientId",
                table: "VitalSigns",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PolicyNumber",
                table: "Patients",
                column: "PolicyNumber",
                unique: true,
                filter: "[PolicyNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "VitalSigns");
            migrationBuilder.DropTable(name: "Relatives");
            migrationBuilder.DropTable(name: "Patients");
        }
    }
}
