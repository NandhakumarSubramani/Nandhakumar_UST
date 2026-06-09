using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Repository.Impl
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HealthAppDBEntities _db;

        public void Add(Appointment appointment)
        {
            _db.Appointments.Add(appointment);
            _db.SaveChanges();
        }

        public List<Appointment> GetAll()
        {
            return _db.Appointments.ToList();

        }

        public Appointment GetById(int id)
        {
            return _db.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        }
    }
}