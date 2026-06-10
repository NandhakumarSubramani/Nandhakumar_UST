using HealthApp.Service.Interface;
using HealthApp.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class HealthRecordController : Controller
    {
        private readonly IHealthRecordApiService _service;

        public HealthRecordController(IHealthRecordApiService service)
        {
            _service = service;
        }

        // GET ALL
        public async Task<ActionResult> HealthRecordsIndex()
        {
            var records = await _service.GetAll();
            return View(records);
        }

        // GET BY PATIENT

        public async Task<ActionResult> GetPatientRecords(int? patientId)
        {
            if (!patientId.HasValue)
                return RedirectToAction("HealthRecordsIndex");

            var data = await _service.GetByPatient(patientId.Value);
            return View("HealthRecordsIndex", data);
        }


        // FILTER
        public async Task<ActionResult> GetRecordsByDoctorAndPatient(int? doctorId, int? patientId)
        {
            if (!doctorId.HasValue || !patientId.HasValue)
                return RedirectToAction("HealthRecordsIndex");

            var records = await _service.GetByDoctorAndPatient(doctorId.Value, patientId.Value);
            return View(records);
        }

        // CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(HealthRecordDto dto)
        {
            await _service.Create(dto);
            return RedirectToAction("HealthRecordsIndex");
        }
    }
}
