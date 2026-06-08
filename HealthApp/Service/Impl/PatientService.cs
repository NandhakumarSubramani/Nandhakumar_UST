using HealthApp.Models;
using HealthApp.Repository.Interface;
using HealthApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Service.Impl
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public void RegisterPatient(Patient patient)
        {
            if (patient == null)
            {
                throw new Exception("Patient cannot be null");
            }
            if (string.IsNullOrWhiteSpace(patient.FullName))
            {
                throw new Exception("Patient name is required");
            }
            if (string.IsNullOrWhiteSpace(patient.Email))
            {
                throw new Exception("Email is required");
            }
            var patients = _repo.GetAll();
            bool emailExists = patients.Any(p => p.Email.ToLower() == patient.Email.ToLower());

            if (emailExists)
            {
                throw new Exception("Email already exists");
            }
            _repo.Add(patient);
        }
        public List<Patient> GetAll()
        {
            return _repo.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            var patient = _repo.GetById(id);

            if (patient == null)
            {
                throw new Exception($"Patient with id {id} not found");
            }
            
            return patient;
        }

        public void UpdatePatientById(int id, Patient patient)
        {
            var existingPatient = _repo.GetById(id);

            if (existingPatient == null)
            {
                throw new Exception($"Patient with id {id} not found");
            }
            patient.PatientId = id;
            _repo.UpdatePatient(id, patient);
        }
    }
}