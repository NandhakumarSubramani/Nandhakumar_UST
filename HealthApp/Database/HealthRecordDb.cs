using HealthApp.Models;
using System;
using System.Collections.Generic;

namespace HealthApp.Database
{
    public static class HealthRecordDb
    {
        public static List<HealthRecord> Records { get; set; }
        static HealthRecordDb()
        {
            Records = new List<HealthRecord>
            {
                new HealthRecord
                {
                    RecordId = 1,
                    Patient = PatientDb.Patients[0],
                    Doctor = DoctorDb.Doctors[0],
                    VisitDate = DateTime.Now.AddDays(-10),
                    Diagnosis = "Fever",
                    Prescription = "Paracetamol",
                    Notes = "Take rest"
                },
                new HealthRecord
                {
                    RecordId = 2,
                    Patient = PatientDb.Patients[1],
                    Doctor = DoctorDb.Doctors[1],
                    VisitDate = DateTime.Now.AddDays(-9),
                    Diagnosis = "Heart Pain",
                    Prescription = "ECG + Tablets",
                    Notes = "Avoid stress"
                },
                new HealthRecord
                {
                    RecordId = 3,
                    Patient = PatientDb.Patients[2],
                    Doctor = DoctorDb.Doctors[2],
                    VisitDate = DateTime.Now.AddDays(-8),
                    Diagnosis = "Skin Allergy",
                    Prescription = "Cream + Tablets",
                    Notes = "Avoid dust"
                },
                new HealthRecord
                {
                    RecordId = 4,
                    Patient = PatientDb.Patients[3],
                    Doctor = DoctorDb.Doctors[3],
                    VisitDate = DateTime.Now.AddDays(-7),
                    Diagnosis = "Migraine",
                    Prescription = "Pain Killers",
                    Notes = "Regular sleep"
                },
                new HealthRecord
                {
                    RecordId = 5,
                    Patient = PatientDb.Patients[0],
                    Doctor = DoctorDb.Doctors[4],
                    VisitDate = DateTime.Now.AddDays(-6),
                    Diagnosis = "Joint Pain",
                    Prescription = "Calcium Tablets",
                    Notes = "Exercise daily"
                },
                new HealthRecord
                {
                    RecordId = 6,
                    Patient = PatientDb.Patients[1],
                    Doctor = DoctorDb.Doctors[5],
                    VisitDate = DateTime.Now.AddDays(-5),
                    Diagnosis = "Cold",
                    Prescription = "Syrup",
                    Notes = "Warm water"
                },
                new HealthRecord
                {
                    RecordId = 7,
                    Patient = PatientDb.Patients[2],
                    Doctor = DoctorDb.Doctors[6],
                    VisitDate = DateTime.Now.AddDays(-4),
                    Diagnosis = "Stress",
                    Prescription = "Therapy",
                    Notes = "Relaxation needed"
                },
                new HealthRecord
                {
                    RecordId = 8,
                    Patient = PatientDb.Patients[3],
                    Doctor = DoctorDb.Doctors[7],
                    VisitDate = DateTime.Now.AddDays(-3),
                    Diagnosis = "Ear Pain",
                    Prescription = "Drops",
                    Notes = "Avoid water"
                },
                new HealthRecord
                {
                    RecordId = 9,
                    Patient = PatientDb.Patients[0],
                    Doctor = DoctorDb.Doctors[8],
                    VisitDate = DateTime.Now.AddDays(-2),
                    Diagnosis = "Pregnancy Check",
                    Prescription = "Vitamins",
                    Notes = "Routine check"
                },
                new HealthRecord
                {
                    RecordId = 10,
                    Patient = PatientDb.Patients[1],
                    Doctor = DoctorDb.Doctors[0],
                    VisitDate = DateTime.Now.AddDays(-1),
                    Diagnosis = "General Checkup",
                    Prescription = "None",
                    Notes = "Healthy"
                }
            };
        }
    }
}
