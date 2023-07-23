using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back.Data;
using back.Models;
using Microsoft.AspNetCore.Authorization;
using back.Dtos;
using back.Services;
using System.Security.Claims;

namespace back.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentsService = appointmentService; 
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var appointments = await _appointmentsService.GetAppointmentsAsync();
            return Ok(appointments);
        }

        // GET: api/Appointments
        [HttpGet("users-appointments/{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetUsersAppointments(long id)
        {
            var appointments = await _appointmentsService.GetUsersAppointments(id);
            return Ok(appointments);
        }

        // [Authorize(Policy = "PatientPolicy")]
        [HttpPost("{id}/get-recommended")]
        public async Task<ActionResult<Appointment>> GetRecommendedAppointment(long id, [FromBody] RecommendedParams recommendedParams)
        {

            var appointment = await _appointmentsService.GetReccomendedAppointments(recommendedParams, id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/reserve/5/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("reserve/{id1}/{id2}")]
        public async Task<IActionResult> ReserveAppointment(long id1, long id2)
        {
            var appointment = await _appointmentsService.ReserveAppointmentAsync(id1, id2);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // PUT: api/Appointments/cancel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(long id)
        {
            var appointment = await _appointmentsService.CancelAppointment(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

    }
}
