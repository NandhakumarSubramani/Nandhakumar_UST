using AutoMapper;
using HealthApp.API.Constant;
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
    [RoutePrefix("api/doctors")]

    public class DoctorApiController : ApiController
    {
        private readonly DoctorService _service;
        private readonly IMapper _mapper;
        public DoctorApiController()
        {
            var db = new HealthAppDBEntities();
            _service = new DoctorService(new DoctorRepository(db));

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

            var doctors = _service.GetAllDoctors();
            var result = _mapper.Map<List<DoctorDto>>(doctors);

            return Ok(result);

           // return Ok(_service.GetAllDoctors());
        }

        // ✅ GET BY ID
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var doctor = _service.GetDoctorById(id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        // ✅ CREATE
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _service.AddDoctor(doctor);

                return Ok("Doctor added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ SEARCH BY SPECIALISATION
        [HttpGet]
        [Route("specialisation/{type}")]
        public IHttpActionResult SearchBySpecialisation(SpecialisationType type)
        {
            var result = _service.SearchBySpecialisation(type);
            return Ok(result);
        }

        // ✅ TOGGLE ACTIVE / INACTIVE
        [HttpPut]
        [Route("{id}/toggle")]
        public IHttpActionResult ToggleStatus(int id)
        {
            _service.ChangeDoctorStatus(id);
            return Ok("Doctor status updated");
        }
    }
}