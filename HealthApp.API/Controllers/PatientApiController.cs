using AutoMapper;
using HealthApp.API.Data;
using HealthApp.API.DTOs;
using HealthApp.API.Mapping;
using HealthApp.API.Repository.Impl;
using HealthApp.API.Service.Impl;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HealthApp.API.Controllers
{
    [RoutePrefix("api/patients")]
    public class PatientApiController : ApiController
    {
        private readonly PatientService _service;
        private readonly IMapper _mapper;

        public PatientApiController()
        {
            var db = new HealthAppDBEntities(); 
            _service = new PatientService(new PatientRepository(db));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); });

            _mapper = config.CreateMapper();

        }



        // ✅ GET ALL
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {

            var patients = _service.GetAll();
            var result = _mapper.Map<List<PatientDto>>(patients);

            return Ok(result);

           // return Ok(_service.GetAll());
        }

        // ✅ GET BY ID
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var patient = _service.GetPatientById(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // ✅ CREATE
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(Patient patient)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _service.RegisterPatient(patient);

                return Ok("Patient created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ UPDATE
        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(int id, Patient patient)
        {
            try
            {
                _service.UpdatePatientById(id, patient);
                return Ok("Patient updated successfully");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

        }
    }
}