using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Repository.Impl
{
    public class PatientRepository : IPatientRepository
    {

        private readonly HealthAppDBEntities _db;
        public PatientRepository(HealthAppDBEntities context)
        {
            _db = context;
        }

        public void Add(Patient patient)
        {
            _db.Patients.Add(patient);
            _db.SaveChanges();
        }

        public List<Patient> GetAll()
        {
            return _db.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return _db.Patients.FirstOrDefault(pa => pa.PatientId == id);
        }
        public void UpdatePatient(int id, Patient patient)
        {
            var p = _db.Patients.FirstOrDefault(pa => pa.PatientId == id);

            if (p == null)
            {
                return;
            }

            p.FullName = patient.FullName;
            p.DateOfBirth = patient.DateOfBirth;
            p.Gender = patient.Gender;
            p.PhoneNumber = patient.PhoneNumber;
            p.Email = patient.Email;
            p.InsuranceId = patient.InsuranceId;

            _db.SaveChanges();
        }
    }
}