using HealthApp.Models;
using HealthApp.Service.Interface;
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
            _service.AddDoctor(doctor);
            return RedirectToAction("DoctorIndex");
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

    }
}