using HealthApp.API.Data;
using HealthApp.API.Repository.Impl;
using HealthApp.API.Service.Impl;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HealthApp.API.Controllers
{
    public class AppointmentApiController : ApiController
    {
        private readonly AppointmentService _service;

        public AppointmentApiController()
        {
            _service = new AppointmentService(new AppointmentRepository());
        }

        // ✅ GET all
        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return _service.GetAllAppointments();
        }

        // ✅ GET by Id
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var data = _service.GetAppointmentById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // ✅ POST
        [HttpPost]
        public IHttpActionResult Post(Appointment appointment)
        {
            try
            {
                _service.Add(appointment);
                return Ok("Appointment created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}