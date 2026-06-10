using AutoMapper;
using HealthApp.API.Data;
using HealthApp.API.Repository.Interface;
using HealthApp.API.Service.Interface;
using HealthApp.Shared.Constant;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthApp.API.Service.Impl
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly HealthAppDBEntities _db;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository repo,
                                  HealthAppDBEntities db,
                                  IMapper mapper)
        {
            _repo = repo;
            _db = db;
            _mapper = mapper;
        }

        // CREATE
        public void Add(AppointmentDto dto)
        {
            if (dto.ScheduledDate < DateTime.Today)
                throw new Exception("Cannot book past date.");

            var doctor = _db.Doctors.FirstOrDefault(d => d.DoctorId == dto.DoctorId);

            if (doctor == null || !(doctor.IsActive ?? false))
                throw new Exception("Doctor unavailable.");

            if (!TimeSlots.Slots.Contains(dto.TimeSlot))
                throw new Exception("Invalid slot.");

            if (dto.ScheduledDate == DateTime.Today)
            {
                DateTime slotTime = DateTime.ParseExact(dto.TimeSlot,
                                                        "hh:mm tt",
                                                        CultureInfo.InvariantCulture);

                DateTime finalTime = dto.ScheduledDate + slotTime.TimeOfDay;

                if (finalTime < DateTime.Now)
                    throw new Exception("Slot already over.");
            }

            bool alreadyBooked = _repo.GetAll().Any(a =>
                a.DoctorId == dto.DoctorId &&
                a.ScheduledDate.Date == dto.ScheduledDate.Date &&
                a.TimeSlot == dto.TimeSlot &&
                a.Status != AppointmentStatus.Cancelled);

            if (alreadyBooked)
                throw new Exception("Slot already booked.");

            var appointment = _mapper.Map<Appointment>(dto);
            appointment.Status = AppointmentStatus.Pending;

            _repo.Add(appointment);
        }

        // GET ALL
        public List<AppointmentDto> GetAllAppointments()
        {
            var list = _repo.GetAll();
            return _mapper.Map<List<AppointmentDto>>(list);
        }

        // GET BY ID
        public AppointmentDto GetAppointmentById(int id)
        {
            var appointment = _repo.GetById(id);

            if (appointment == null)
                throw new Exception($"Appointment with id {id} not found");

            return _mapper.Map<AppointmentDto>(appointment);
        }

        // CANCEL
        public void CancelAppointment(int appointmentId, string reason)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);


            if (appointment == null)
                throw new Exception("Appointment not found");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new Exception("Already cancelled");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new Exception("Cannot cancel completed appointment");

          
            appointment.Status = "Cancelled";
            appointment.CancellationReason = reason;
            _db.SaveChanges();
        }

        // CONFIRM
        public void ConfirmAppointment(int appointmentId)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null)
                throw new Exception("Appointment not found");

            if (appointment.Status == AppointmentStatus.Cancelled)
                throw new Exception("Cancelled appointment cannot be confirmed");

            if (appointment.Status == AppointmentStatus.Completed)
                throw new Exception("Already completed");

            if (appointment.Status == AppointmentStatus.Confirmed)
                throw new Exception("Already confirmed");

            appointment.Status = AppointmentStatus.Confirmed;
            _db.SaveChanges();
        }

        // PATIENT APPOINTMENTS
        public List<AppointmentDto> GetAppointmentsByPatient(int patientId)
        {
            var list = _repo.GetAll()
                            .Where(a => a.PatientId == patientId)
                            .OrderBy(a => a.ScheduledDate)
                            .ThenBy(a => a.TimeSlot)
                            .ToList();

            return _mapper.Map<List<AppointmentDto>>(list);
        }

        // DOCTOR UPCOMING
        public List<AppointmentDto> GetUpcomingAppointmentsByDoctor(int doctorId, DateTime fromDate, DateTime toDate)
        {
            if (fromDate < DateTime.Today)
                throw new Exception("From date cannot be in the past");

            if (fromDate > toDate)
                throw new Exception("Invalid date range");

            var list = _repo.GetAll()
                .Where(a => a.DoctorId == doctorId &&
                            a.ScheduledDate >= fromDate &&
                            a.ScheduledDate <= toDate &&
                            a.Status == AppointmentStatus.Confirmed)
                .OrderBy(a => a.ScheduledDate)
                .ThenBy(a => a.TimeSlot)
                .ToList();

            return _mapper.Map<List<AppointmentDto>>(list);
        }

        // DOCTOR PENDING
        public List<AppointmentDto> GetPendingAppointmentsByDoctor(int doctorId)
        {
            var list = _repo.GetAll()
                .Where(a => a.DoctorId == doctorId &&
                            a.Status == AppointmentStatus.Pending)
                .OrderBy(a => a.ScheduledDate)
                .ThenBy(a => a.TimeSlot)
                .ToList();

            return _mapper.Map<List<AppointmentDto>>(list);
        }

        // CHECK AVAILABILITY
        public List<string> CheckDoctorAvailability(int doctorId, DateTime date)
        {
            if (date < DateTime.Today)
                throw new Exception("Date already passed");

            if (date > DateTime.Today.AddDays(90))
                throw new Exception("Only next 90 days allowed");

            var bookedSlots = _repo.GetAll()
                .Where(a => a.DoctorId == doctorId &&
                            a.ScheduledDate.Date == date.Date &&
                            a.Status != AppointmentStatus.Cancelled)
                .Select(a => a.TimeSlot)
                .ToList();

            var availableSlots = TimeSlots.Slots.Except(bookedSlots).ToList();

            if (availableSlots.Count == 0)
                throw new Exception("No slots available");

            return availableSlots;
        }

        // COMPLETE
        public void CompleteAppointment(int appointmentId)
        {
            var appointment = _db.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null)
                throw new Exception("Appointment not found");

            if (appointment.Status != AppointmentStatus.Confirmed)
                throw new Exception("Only confirmed appointment can be completed");

            appointment.Status = AppointmentStatus.Completed;

            _db.SaveChanges();
        }

    }
}
