using HealthApp.Models;
using HealthApp.Service.Interface;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }
        public ActionResult PatientIndex()
        {
            var patients = _service.GetAll();
            return View(patients);
        }

        public ActionResult GetById(int id)
        {
            var patient = _service.GetPatientById(id);
            return View(patient);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            _service.RegisterPatient(patient);
            return RedirectToAction("PatientIndex");
        }

        public ActionResult Edit(int id)
        {
            var patient = _service.GetPatientById(id);
            return View(patient);
        }

        [HttpPost]
        public ActionResult Edit(int id, Patient patient)
        {
            _service.UpdatePatientById(id, patient);
            return RedirectToAction("PatientIndex");
        }
    }
}