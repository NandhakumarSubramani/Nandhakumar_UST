using HealthApp.API.Data;
using HealthApp.API.Constant;
using HealthApp.API.Repository.Interface;
using HealthApp.API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Service.Impl
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

            string spec = specialisation.ToString();

            return _repo.GetAll()
                .Where(d => d.Specialisation == spec ).ToList();
             //.Where(d => d.Specialisation == specialisation && d.IsActive).ToList();

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