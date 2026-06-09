using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Repository.Impl
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealthAppDBEntities _db;
        public DoctorRepository(HealthAppDBEntities context)
        {
            _db = context;
        }

        public void Add(Doctor doctor)
        {
            _db.Doctors.Add(doctor);
            _db.SaveChanges();
        }

        public List<Doctor> GetAll()
        {
            return _db.Doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            var doctor = _db.Doctors
                .FirstOrDefault(d => d.DoctorId == id);

            return doctor;
        }

        public void Update(Doctor doctor)
        {
            var existingDoctor = _db.Doctors.FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (existingDoctor != null)
            {
                existingDoctor.IsActive = doctor.IsActive;
            }
            _db.SaveChanges();
        }
    }
}