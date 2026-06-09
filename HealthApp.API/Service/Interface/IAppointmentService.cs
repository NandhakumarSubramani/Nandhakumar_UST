using HealthApp.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthApp.API.Service.Interface
{
    public interface IAppointmentService
    {
        void Add(Appointment appointment);

        void CancelAppointment(
            int appointmentId,
            string reason);

        Appointment GetAppointmentById(int id);
        List<Appointment> GetAllAppointments();
        List<Appointment> GetAppointmentsByPatient(int patientId);
        List<string> CheckDoctorAvailability(int doctorId, DateTime date);
        List<Appointment> GetUpcomingAppointmentsByDoctor(int doctorId, DateTime fromDate, DateTime toDate);
        List<Appointment> GetPendingAppointmentsByDoctor(int doctorId);
        void ConfirmAppointment(int appointmentId);
    }
}