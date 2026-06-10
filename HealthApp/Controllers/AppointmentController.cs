using HealthApp.Service.Interface;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentApiService _service;

        public AppointmentController(IAppointmentApiService service)
        {
            _service = service;
        }

        // GET ALL
        public async Task<ActionResult> AppointmentIndex()
        {
            var list = await _service.GetAll();
            return View(list);
        }

        // GET BY ID
        public async Task<ActionResult> GetById(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("AppointmentIndex");

            var data = await _service.GetById(id.Value);
            return View("AppointmentIndex", new List<AppointmentDto> { data });
        }

        // GET BY PATIENT
        public async Task<ActionResult> GetByPatientID(int? patientId)
        {
            if (!patientId.HasValue)
                return RedirectToAction("AppointmentIndex");

            var list = await _service.GetByPatient(patientId.Value);
            return View("AppointmentIndex", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm(int id)
        {
            await _service.Confirm(id);

            TempData["Success"] = "Appointment confirmed!";
            return RedirectToAction("AppointmentIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cancel(int id)
        {
            await _service.Cancel(id, "Cancelled by user");

            TempData["Success"] = "Appointment cancelled!";
            return RedirectToAction("AppointmentIndex");
        }

        // CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Create(AppointmentDto dto)
        {
            if (!ModelState.IsValid)
            {

                TempData["Error"] = "Please fill all required fields correctly!";
                return View(dto);

            }
            await _service.Create(dto);

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction("AppointmentIndex");

        }


        // CHECK AVAILABILITY
        public async Task<ActionResult> CheckAvailability(int? doctorId, DateTime? date)
        {
            if (!doctorId.HasValue || !date.HasValue)
                return RedirectToAction("AppointmentIndex");

            var slots = await _service.CheckAvailability(doctorId.Value, date.Value);

            return View(slots);
        }
    }
}
