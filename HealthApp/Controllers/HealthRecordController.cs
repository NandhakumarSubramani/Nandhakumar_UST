using HealthApp.Database;
using HealthApp.Models;
using HealthApp.Service.Interface;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class HealthRecordController : Controller
    {
        private readonly IHealthRecordService _service;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public HealthRecordController(
            IHealthRecordService service,
            IPatientService patientService,
            IDoctorService doctorService)
        {
            _service = service;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        public ActionResult HealthRecordsIndex()
        {
            var records = _service.GetAllRecords();
            return View("HealthRecordsIndex", records);
        }

        public ActionResult GetPatientRecords(int? patientId)
        {
            if (patientId == null)
            {
                return RedirectToAction("HealthRecordsIndex");
            }
            var records = _service.GetPatientRecords(patientId.Value);
            return View("GetPatientRecords", records);
        }

        public ActionResult GetRecordsByDoctorAndPatient(int? doctorId, int? patientId)
        {
            if (!doctorId.HasValue && !patientId.HasValue)
            {
                return RedirectToAction("HealthRecordsIndex");
            }
            var records = _service.GetAllRecords().AsQueryable();
            if (doctorId.HasValue)
            {
                records = records.Where(r => r.Doctor.DoctorId == doctorId.Value);
            }
            if (patientId.HasValue)
            {
                records = records.Where(r => r.Patient.PatientId == patientId.Value);
            }
            return View("GetRecordsByDoctorAndPatient", records.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HealthRecord record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(record);
                }

                if (record.Patient == null || record.Patient.PatientId == 0)
                {
                    ModelState.AddModelError("Patient.PatientId", "Patient Id is required");
                    return View(record);
                }

                if (record.Doctor == null || record.Doctor.DoctorId == 0)
                {
                    ModelState.AddModelError("Doctor.DoctorId", "Doctor Id is required");
                    return View(record);
                }
                var patient = _patientService.GetPatientById(record.Patient.PatientId);
                var doctor = _doctorService.GetDoctorById(record.Doctor.DoctorId);

                record.Patient = patient;
                record.Doctor = doctor;

                var existing = _service.GetAllRecords().Any(r =>
                    r.Patient.PatientId == record.Patient.PatientId &&
                    r.Doctor.DoctorId == record.Doctor.DoctorId &&
                    r.VisitDate.Date == record.VisitDate.Date);
                if (existing)
                {
                    ModelState.AddModelError("", "Record already exists!");
                    return View(record);
                }
                _service.AddRecord(record);
                return RedirectToAction("HealthRecordsIndex");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(record);
            }
        }
        public ActionResult CreateFromAppointment(int appointmentId)
        {
            var appointment = AppointmentDb.Appointments
                .FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment == null)
            {
                return RedirectToAction("AppointmentIndex", "Appointment");
            }
            var record = new HealthRecord
            {
                Patient = appointment.Patient,
                Doctor = appointment.Doctor,
                VisitDate = appointment.ScheduledDate
            };
            return View(record);
        }

        [HttpPost]
        public ActionResult CreateFromAppointment(HealthRecord record)
        {
            var patient = _patientService.GetPatientById(record.Patient.PatientId);
            var doctor = _doctorService.GetDoctorById(record.Doctor.DoctorId);

            record.Patient = patient;
            record.Doctor = doctor;

            _service.AddRecord(record);

            var appointment = AppointmentDb.Appointments
                .FirstOrDefault(a => a.Patient.PatientId == record.Patient.PatientId
                    && a.Doctor.DoctorId == record.Doctor.DoctorId
                    && a.ScheduledDate.Date == record.VisitDate.Date);

            appointment?.Complete();

            return RedirectToAction("AppointmentIndex", "Appointment");
        }

    }
}
