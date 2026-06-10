using HealthApp.API.Constant;
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
    [RoutePrefix("api/doctors")]
    public class DoctorApiController : ApiController
    {

        private readonly IDoctorService _service;
        public DoctorApiController(IDoctorService service)
        {
            _service = service;
        }


        // GET all doctors
        [HttpGet]
        [Route("")]
        public IEnumerable<DoctorDto> GetAll()
        {
            return _service.GetAllDoctors();
        }

        // GET doctor by ID
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var doctor = _service.GetDoctorById(id);
                return Ok(doctor);
            }
            catch
            {
                return NotFound();
            }
        }

        // CREATE doctor
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(DoctorDto dto)
        {
            try
            {
                _service.AddDoctor(dto);
                return Ok("Doctor added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // SEARCH by specialisation
        [HttpGet]
        [Route("specialisation/{type}")]
        public IHttpActionResult SearchBySpecialisation(SpecialisationType type)
        {
            var result = _service.SearchBySpecialisation(type);
            return Ok(result);
        }

        // TOGGLE active/inactive
        [HttpPut]
        [Route("{id}/toggle")]
        public IHttpActionResult ToggleStatus(int id)
        {
            try
            {
                _service.ChangeDoctorStatus(id);
                return Ok("Doctor status updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}