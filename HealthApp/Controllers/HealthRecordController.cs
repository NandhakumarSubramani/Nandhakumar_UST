using HealthApp.Service.Interface;
using HealthApp.Shared.DTOs;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class HealthRecordController : Controller
    {
        private readonly IHealthRecordApiService _service;
        private readonly IAppointmentApiService _appointmentService;

        public HealthRecordController(
            IHealthRecordApiService service,
            IAppointmentApiService appointmentService)
        {
            _service = service;
            _appointmentService = appointmentService;
        }

        // ✅ GET Health Records
        public async Task<ActionResult> HealthRecordsIndex()
        {
            var records = await _service.GetAll();
            return View(records);
        }

        // ✅ CREATE (manual)
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

        //  GET: Create from Appointment
        public async Task<ActionResult> CreateFromAppointment(int appointmentId)
        {
            var appointment = await _appointmentService.GetById(appointmentId);

            var model = new HealthRecordDto
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                PatientName = appointment.PatientName,
                DoctorName = appointment.DoctorName,
                VisitDate = appointment.ScheduledDate
            };

            return View(model);
        }

        // ✅ ✅ POST: Create from Appointment (ONLY ONE)
        [HttpPost]
        public async Task<ActionResult> CreateFromAppointment(HealthRecordDto dto)
        {
            await _service.Create(dto);

            // ✅ Mark appointment completed
            await _appointmentService.MarkCompleted(dto.AppointmentId);

            return RedirectToAction("AppointmentIndex", "Appointment");
        }
    }
}