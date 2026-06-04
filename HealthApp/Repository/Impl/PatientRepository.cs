using HealthApp.Database;
using HealthApp.Models;
using HealthApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Repository.Impl
{
    public class PatientRepository : IPatientRepository
    {

        public void Add(Patient patient)
        {
            PatientDb.Patients.Add(patient);
        }

        public List<Patient> GetAll()
        {
            return PatientDb.Patients;
        }

        public Patient GetById(int id)
        {
            return PatientDb.Patients.FirstOrDefault(pa => pa.PatientId == id);
        }

        public Patient UpdatePatient(int id, Patient patient)
        {
            var p = PatientDb.Patients.FirstOrDefault(pa => pa.PatientId == id);

            if (p == null)
            {
                return null;
            }

            p.FullName = patient.FullName;
            p.DateOfBirth = patient.DateOfBirth;
            p.Gender = patient.Gender;
            p.PhoneNumber = patient.PhoneNumber;
            p.Email = patient.Email;
            p.InsuranceId = patient.InsuranceId;

            return p;
        }
    }
}