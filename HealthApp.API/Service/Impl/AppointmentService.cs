using HealthApp.API.Constant;
using HealthApp.API.Data;
using HealthApp.API.Repository.Impl;
using HealthApp.API.Repository.Interface;
using HealthApp.API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HealthApp.API.Service.Impl
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HealthAppDBEntities _db;
        private readonly IAppointmentRepository _repo;
        private AppointmentRepository appointmentRepository;

        public AppointmentService(IAppointmentRepository repo, HealthAppDBEntities db)
        {
            _repo = repo;
            _db= db;
        }

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }

        public void Add(Appointment appointments)
        {
            if (appointments.ScheduledDate < DateTime.Today)
            {
                throw new Exception("Cannot book past date.");
            }

            var doctor = _db.Doctors.FirstOrDefault(d=>d.DoctorId == appointments.DoctorId);
            if ((bool)!doctor.IsActive)
            {
                throw new Exception("Doctor unavailable.");
            }

            if (!TimeSlots.Slots.Contains(appointments.TimeSlot))
            {
                throw new Exception("Invalid slot.");
            }

            if (appointments.ScheduledDate == DateTime.Today)
            {
                DateTime slotTime = DateTime.ParseExact(appointments.TimeSlot, "hh:mm tt", CultureInfo.InvariantCulture);

                DateTime finalTime = appointments.ScheduledDate + slotTime.TimeOfDay;

                if (finalTime < DateTime.Now)
                {
                    throw new Exception("Slot already over.");
                }
            }

            bool sameDoctorBooked = _repo.GetAll().Any(a => a.Patient.PatientId == appointments.Patient.PatientId
                && a.Doctor.DoctorId == appointments.Doctor.DoctorId && a.ScheduledDate.Date == appointments.ScheduledDate
                && a.Status != AppointmentStatus.Cancelled);

            if (sameDoctorBooked)
            {
                throw new Exception("You already booked an appointment with this doctor today.");
            }


            bool sameSlotBooked = _repo.GetAll().Any(a => a.Patient.PatientId == appointments.Patient.PatientId
                && a.ScheduledDate.Date == appointments.ScheduledDate && a.TimeSlot == appointments.TimeSlot
                && a.Status != "Cancelled");

            if (sameSlotBooked)
            {
                throw new Exception("You already have another appointment in this time slot.");
            }

            bool alreadyBooked = _repo.GetAll().Any(a => a.Doctor.DoctorId == appointments.Doctor.DoctorId
                    && a.ScheduledDate.Date == appointments.ScheduledDate && a.TimeSlot == appointments.TimeSlot
                    && a.Status != AppointmentStatus.Cancelled);

            if (alreadyBooked)
            {
                throw new Exception("Slot already booked.");
            }

            Appointment appointment = new Appointment
            {


                Patient = appointments.Patient,

                Doctor = appointments.Doctor,

                ScheduledDate = appointments.ScheduledDate,

                TimeSlot = appointments.TimeSlot,

                Status = AppointmentStatus.Pending
            };

            _repo.Add(appointment);

        }

        public void CancelAppointment(int appointmentId, string reason)
        {
            var appointment = _repo.GetById(appointmentId);

            if (appointment == null)
            {
                throw new Exception($"The appointment with id {appointmentId} not found");
            }

            if (appointment.Status == AppointmentStatus.Cancelled)
            {

                throw new Exception("This appointment was already cancelled.");
            }

            if (appointment.Status == AppointmentStatus.Completed)
            {
                throw new Exception("Completed appointments cannot be cancelled.");
            }

            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.Cancelled;
                appointment.CancellationReason = reason;
            }
        }

        public Appointment GetAppointmentById(int id)
        {
            var appointment = _repo.GetById(id);

            if (appointment == null)
            {
                throw new Exception($"Appointment with id {id} not found");
            }

            return appointment;
        }

        public List<Appointment> GetAllAppointments()
        {
            var list = _repo.GetAll();

            if (list == null || list.Count == 0)
            {
                throw new Exception("No appointments found");
            }

            return list;
        }

        public List<Appointment> GetAppointmentsByPatient(int patientId)
        {
            return _repo.GetAll().Where(a => a.Patient.PatientId == patientId &&
                (a.Status == AppointmentStatus.Confirmed || a.Status == AppointmentStatus.Pending
                || a.Status == AppointmentStatus.Cancelled) && a.ScheduledDate.Date >= DateTime.Today)
                .OrderBy(a => a.ScheduledDate).ThenBy(a => a.TimeSlot).ToList();
        }
        public List<Appointment> GetUpcomingAppointmentsByDoctor(int doctorId, DateTime fromDate,
            DateTime toDate)
        {

            if (fromDate.Date < DateTime.Today)
            {
                throw new Exception("From date cannot be in the past");
            }

            if (fromDate > toDate)
            {
                throw new Exception("Invalid date range");
            }

            var result = _repo.GetAll().Where(a => a.Doctor.DoctorId == doctorId && a.Status
                == AppointmentStatus.Confirmed && a.ScheduledDate.Date >= fromDate.Date
                && a.ScheduledDate.Date <= toDate.Date && a.ScheduledDate.Date >= DateTime.Today)
                .OrderBy(a => a.ScheduledDate).ThenBy(a => a.TimeSlot).ToList();

            if (result.Count == 0)
            {
                throw new Exception($"No upcoming appointments found for doctor id {doctorId}");
            }

            return result;
        }

        public List<string> CheckDoctorAvailability(int doctorId, DateTime date)
        {
            if (date.Date < DateTime.Today)
            {
                throw new Exception("The selected date is already over.");
            }

            if (date.Date > DateTime.Today.AddDays(90))
            {
                throw new Exception("Appointments can only be checked within 90 days from today.");
            }

            var bookedSlots = _repo.GetAll().Where(a => a.Doctor.DoctorId == doctorId &&
                    a.ScheduledDate.Date == date.Date && a.Status != AppointmentStatus.Cancelled)
                    .Select(a => a.TimeSlot).ToList();


            var availableSlots = TimeSlots.Slots.Except(bookedSlots).ToList();


            if (availableSlots.Count == 0)
            {
                throw new Exception("No available slots on this day.");
            }

            return availableSlots;
        }

        public List<Appointment> GetPendingAppointmentsByDoctor(int doctorId)
        {
            var result = _repo.GetAll().Where(a => a.Doctor.DoctorId == doctorId &&
                 a.Status == AppointmentStatus.Pending).OrderBy(a => a.ScheduledDate)
                .ThenBy(a => a.TimeSlot).ToList();
            if (result.Count == 0)
            {
                throw new Exception($"No existing appointments for doctor with id {doctorId}");
            }
            return result;
        }

        public void ConfirmAppointment(int appointmentId)
        {
            var appointment = _repo.GetById(appointmentId);

            if (appointment == null)
            {
                throw new Exception($"Appointment with id {appointmentId} not found");
            }

            if (appointment.Status == AppointmentStatus.Cancelled)
            {
                throw new Exception(
                    "Cancelled appointment cannot be confirmed.");
            }
            if (appointment.Status == AppointmentStatus.Completed)
            {
                throw new Exception(
                    "The appointment is already completed");
            }
            if (appointment.Status == AppointmentStatus.Confirmed)
            {
                throw new Exception(
                    "You have already confirmed this appointment");
            }

            appointment.Status = AppointmentStatus.Confirmed;
        }
    }
}