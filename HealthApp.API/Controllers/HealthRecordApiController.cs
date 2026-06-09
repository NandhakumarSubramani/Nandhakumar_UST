using HealthApp.API.Data;
using HealthApp.API.Repository.Impl;
using HealthApp.API.Service.Impl;
using System;
using System.Web.Http;

namespace HealthApp.API.Controllers
{
    [RoutePrefix("api/healthrecords")]
    public class HealthRecordApiController : ApiController
    {
        private readonly HealthRecordService _service;

        public HealthRecordApiController()
        {
            _service = new HealthRecordService(new HealthRecordRepository());
        }

        // ✅ GET ALL
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllRecords());
        }

        // ✅ GET BY PATIENT
        [HttpGet]
        [Route("patient/{id}")]
        public IHttpActionResult GetByPatient(int id)
        {
            return Ok(_service.GetPatientRecords(id));
        }

        // ✅ FILTER
        [HttpGet]
        [Route("filter")]
        public IHttpActionResult GetByDoctorAndPatient(int doctorId, int patientId)
        {
            return Ok(_service.GetHealthRecordsByDoctor(doctorId, patientId));
        }

        // ✅ CREATE
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(HealthRecord record)
        {
            try
            {
                if (record == null)
                    return BadRequest("Invalid data");

                _service.AddRecord(record);

                return Ok("Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}