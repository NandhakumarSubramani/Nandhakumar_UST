using HealthApp.API.Data;
using HealthApp.API.Repository.Impl;
using HealthApp.API.Service.Impl;
using HealthApp.API.Service.Interface;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HealthApp.API.Controllers
{
    public class PatientApiController : ApiController
    {

        private readonly IPatientService _service;
        public PatientApiController(IPatientService service)
        {
            _service = service;
        }


        // GET all patients
        [HttpGet]
        public IEnumerable<PatientDto> Get()
        {
            return _service.GetAll();
        }

        // GET patient by ID
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var patient = _service.GetPatientById(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return Content(System.Net.HttpStatusCode.NotFound, ex.Message);
            }
        }

        // CREATE new patient
        [HttpPost]
        public IHttpActionResult Post(PatientDto dto)
        {
            try
            {
                _service.RegisterPatient(dto);
                return Ok("Patient registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE patient
        [HttpPut]
        public IHttpActionResult Put(int id, PatientDto dto)
        {
            try
            {
                _service.UpdatePatientById(id, dto);
                return Ok("Patient updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}