using HealthApp.API.Repository.Impl;
using HealthApp.API.Service.Impl;
using HealthApp.API.Service.Interface;
using HealthApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace HealthApp.API.Controllers
{
    public class AppointmentApiController : ApiController
    {

        private readonly IAppointmentService _service;

        public AppointmentApiController(IAppointmentService service)
        {
            _service = service;
        }


        // GET all
        [HttpGet]
        public IEnumerable<AppointmentDto> Get()
        {
            return _service.GetAllAppointments();
        }

        // GET by ID
        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var data = _service.GetAppointmentById(id);
                return Ok(data);
            }
            catch
            {
                return NotFound();
            }
        }

        // CREATE
        [HttpPost]
        public IHttpActionResult Post(AppointmentDto dto)
        {
            try
            {
                _service.Add(dto);
                return Ok("Appointment created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // CANCEL
        [HttpPut]
        [Route("api/appointments/{id}/cancel")]
        public IHttpActionResult Cancel(int id, string reason)
        {
            try
            {
                _service.CancelAppointment(id, reason);
                return Ok("Appointment cancelled");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // CONFIRM
        [HttpPut]
        [Route("api/appointments/{id}/confirm")]
        public IHttpActionResult Confirm(int id)
        {
            try
            {
                _service.ConfirmAppointment(id);
                return Ok("Appointment confirmed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}