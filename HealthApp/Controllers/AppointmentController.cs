using HealthApp.Database;
using HealthApp.Models;
using HealthApp.Service.Interface;
using System;
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

        public ActionResult GetById(int id)
        {
            var result = _service.GetAppointmentById(id);
            return View(result);
        }

        public ActionResult GetByPatientID(int patientId)
        {
            var result = _service.GetAppointmentsByPatient(patientId);
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

        public ActionResult CheckAvailability(int doctorId, DateTime date)
        {
            var slots = _service.CheckDoctorAvailability(doctorId, date);
            return View(slots);
        }
    }
}