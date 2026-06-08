using HealthApp.API.Models;
using HealthApp.API.Repository.Interface;
using HealthApp.API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Service.Impl
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
            var patients = _repo.GetAll();
            _repo.Add(patient);
        }
        public List<Patient> GetAll()
        {
            return _repo.GetAll();
        }

        public Patient GetPatientById(int id)
        {
            var patient = _repo.GetById(id);
            return patient;
        }
        public string UpdatePatientById(int id, Patient patient)
        {
            var updatedPatient = _repo.UpdatePatient(id, patient);
            return $"Patient with id {id} updated successfully";
        }
    }
}