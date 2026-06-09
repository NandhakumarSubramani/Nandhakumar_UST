using HealthApp.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.API.Repository.Interface
{
    public interface IAppointmentRepository
    {
        void Add(Appointment appointment);
        List<Appointment> GetAll();
        Appointment GetById(int id);
    }
}