using HealthApp.Models;
using HealthApp.Repository.Interface;
using HealthApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.Service.Impl
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public void AddDoctor(Doctor doctor)
        {
            var doctors = _repo.GetAll();
            doctor.IsActive = true;

            _repo.Add(doctor);
        }

        public List<Doctor> GetAllDoctors()
        {
            var result = _repo.GetAll();
            return result;
        }

        public Doctor GetDoctorById(int id)
        {

            var doctor = _repo.GetById(id);
            return doctor;
        }

        public List<Doctor> SearchBySpecialisation(SpecialisationType specialisation)
        {
            var result = _repo.GetAll().Where(d => d.Specialisation == specialisation).ToList();
            return result;
        }
        public void ChangeDoctorStatus(int id)
        {
            var doctor = _repo.GetById(id);

            if (doctor != null)
            {
                doctor.IsActive = !doctor.IsActive;
                _repo.Update(doctor);
            }
        }

    }
}