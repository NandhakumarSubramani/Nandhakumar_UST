using HealthApp.API.Database;
using HealthApp.API.Models;
using HealthApp.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthApp.API.Repository.Impl
{
    public class AppointmentRepository : IAppointmentRepository
    {

        public void Add(Appointment appointment)
        {
            AppointmentDb.Appointments.Add(appointment);
        }
        public List<Appointment> GetAll()
        {
            return AppointmentDb.Appointments;
        }
        public Appointment GetById(int id)
        {
            return AppointmentDb.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        }
    }
}