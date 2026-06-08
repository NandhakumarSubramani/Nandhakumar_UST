using HealthApp.Database;
using HealthApp.Models;
using HealthApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        public ActionResult AppointmentIndex()
        {
            var appointment = _service.GetAllAppointments();
            return View(appointment);
        }

        public ActionResult GetById(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("AppointmentIndex");
            }

            var result = _service.GetAppointmentById(id.Value);

            return View("AppointmentIndex", new List<Appointment> { result });
        }

        public ActionResult GetByPatientID(int? patientId)
        {
            if (!patientId.HasValue)
            {
                return RedirectToAction("AppointmentIndex");
            }

            var result = _service.GetAppointmentsByPatient(patientId.Value);

            return View("AppointmentIndex", result);
        }

        public ActionResult Confirm(int id)
        {
            _service.ConfirmAppointment(id);
            return RedirectToAction("AppointmentIndex");
        }

        public ActionResult Cancel(int id)
        {
            _service.CancelAppointment(id, "Cancelled by user");
            return RedirectToAction("AppointmentIndex");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(int patientId, int doctorId, DateTime date, string slot)
        {
            var patient = PatientDb.Patients.Find(p => p.PatientId == patientId);
            var doctor = DoctorDb.Doctors.Find(d => d.DoctorId == doctorId);
            _service.BookAppointment(patient, doctor, date, slot);
            return RedirectToAction("AppointmentIndex");
        }

        public ActionResult CheckAvailability(int? doctorId, DateTime? date)
        {
            if (!doctorId.HasValue || !date.HasValue)
            {
                return RedirectToAction("AppointmentIndex");
            }

            if (date.Value.Date < DateTime.Today)
            {
                return RedirectToAction("AppointmentIndex");
            }
                var slots = _service.CheckDoctorAvailability(doctorId.Value, date.Value);
                return View(slots);
        }

    }
}