using HealthApp.API.Models;
using HealthApp.API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HealthApp.API.Controllers
{
    [RoutePrefix("api/healthrecords")]
    public class HealthRecordApiController : ApiController
    {

        private AppDbContext _db = new AppDbContext();


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var data =_db.HealthRecords.Include("Patient").Include("Doctor").ToList();
            return Ok(data);
        }

        [HttpGet]
        [Route("patient/{id}")]
        public IHttpActionResult GetByPatient(int id)
        {
            var data = _db.HealthRecords.Where(r => r.PatientId == id).Include(r => r.Doctor).ToList();

            return Ok(data);
        }

        [HttpGet]
        [Route("filter")]
        public IHttpActionResult GetByDoctorAndPatient(int doctorId, int patientId)
        {
            var data =_db.HealthRecords.Where(r => r.DoctorId 
            == doctorId && r.PatientId == patientId).ToList();

            return Ok(data);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(HealthRecord record)
        {
            if (record == null)
                return BadRequest("Invalid Data");

            var patient = _db.GetPatientById(record.Patient.PatientId);
            var doctor = _db.GetDoctorById(record.Doctor.DoctorId);

            if (patient == null || doctor == null)
                return BadRequest("Invalid Patient or Doctor");

            record.Patient = patient;
            record.Doctor = doctor;

            var exists = _db.GetAllRecords().Any(r =>
                r.Patient.PatientId == record.Patient.PatientId &&
                r.Doctor.DoctorId == record.Doctor.DoctorId &&
                r.VisitDate == record.VisitDate);

            if (exists)
                return BadRequest("Record already exists");

            _db.AddRecord(record);

            return Ok("Created Successfully");
        }
    }
}