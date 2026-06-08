using HealthApp.Models;
using HealthApp.Service.Interface;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }
        public ActionResult DoctorIndex()
        {
            var doctors = _service.GetAllDoctors();
            return View(doctors);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(doctor);
                }
                _service.AddDoctor(doctor);
                return RedirectToAction("DoctorIndex");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Email"))
                {
                    ModelState.AddModelError("DoctorEmail", ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", ex.Message);
                }
                return View(doctor);
            }
        }
        public ActionResult SearchBySpecialisation(SpecialisationType? specialisation)
        {
            if (specialisation == null)
            {
                var allDoctors = _service.GetAllDoctors();
                return View("DoctorIndex", allDoctors);
            }
            var doctors = _service.SearchBySpecialisation(specialisation.Value);
            return View("DoctorIndex", doctors); 
        }
        public ActionResult GetById(int id)
        {
            var doctor = _service.GetDoctorById(id);
            return View(doctor);
        }

        public ActionResult ToggleStatus(int id)
        {
            _service.ChangeDoctorStatus(id);
            return RedirectToAction("DoctorIndex");
        }
        public ActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("DoctorIndex");
            }
            if (int.TryParse(query, out int id))
            {
                var doctor = _service.GetDoctorById(id);
                return View("GetById", doctor);
            }
            var doctors = _service.GetAllDoctors().Where(d => d.FullName.ToLower()
                        .Contains(query.ToLower())).ToList();

            return View("DoctorIndex", doctors);
        }
    }
}