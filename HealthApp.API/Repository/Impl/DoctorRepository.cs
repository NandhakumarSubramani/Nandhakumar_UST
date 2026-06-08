using HealthApp.API.Database;
using HealthApp.API.Models;
using HealthApp.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Repository.Impl
{
    public class DoctorRepository : IDoctorRepository
    {

        public void Add(Doctor doctor)
        {
            DoctorDb.Doctors.Add(doctor);
        }

        public List<Doctor> GetAll()
        {
            return DoctorDb.Doctors;
        }

        public Doctor GetById(int id)
        {
            var doctor = DoctorDb.Doctors
                .FirstOrDefault(d => d.DoctorId == id);

            return doctor;
        }

        public void Update(Doctor doctor)
        {
            var existingDoctor = DoctorDb.Doctors.FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (existingDoctor != null)
            {
                existingDoctor.IsActive = doctor.IsActive;
            }
        }
    }
}