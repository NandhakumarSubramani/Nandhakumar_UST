using HealthApp.Models;
using HealthApp.Service.Interface;
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

        public ActionResult GetRecordsByDoctorAndPatient(int doctorId, int patientId)
        {
            var records = _service.GetHealthRecordsByDoctor(doctorId, patientId);
            return View("GetRecordsByDoctorAndPatient", records);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HealthRecord record)
        {
            if (record.Patient == null || record.Patient.PatientId == 0 ||
                record.Doctor == null || record.Doctor.DoctorId == 0)
            {
                return View(record);
            }

            var patient = _patientService.GetPatientById(record.Patient.PatientId);
            var doctor = _doctorService.GetDoctorById(record.Doctor.DoctorId);

            record.Patient = patient;
            record.Doctor = doctor;

            var existing = _service.GetAllRecords().Any(r =>
                r.Patient.PatientId == record.Patient.PatientId &&
                r.Doctor.DoctorId == record.Doctor.DoctorId &&
                r.VisitDate == record.VisitDate);

            if (existing)
            {
                ViewBag.Message = "Record already exists!";
                return View(record);
            }

            _service.AddRecord(record);

            return RedirectToAction("HealthRecordsIndex");
        }
    }
}
