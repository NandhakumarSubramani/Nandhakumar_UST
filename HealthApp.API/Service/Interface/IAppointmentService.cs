using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;

namespace HealthApp.API.Service.Interface
{
    public interface IAppointmentService
    {
        void Add(AppointmentDto dto);
        void CancelAppointment(int appointmentId, string reason);
        AppointmentDto GetAppointmentById(int id);
        List<AppointmentDto> GetAllAppointments();
        List<AppointmentDto> GetAppointmentsByPatient(int patientId);
        List<string> CheckDoctorAvailability(int doctorId, DateTime date);
        List<AppointmentDto> GetUpcomingAppointmentsByDoctor(int doctorId, DateTime fromDate, DateTime toDate);
        List<AppointmentDto> GetPendingAppointmentsByDoctor(int doctorId);
        void ConfirmAppointment(int appointmentId);
    }
}