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
using back.Protos;
using Grpc.Core;

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

        // GET: api/Appointments/Donations
        [HttpGet("donations")]
        public async Task<ActionResult> GetDonations()
        {

            var response = await _appointmentsService.GetDonationsAsync();
            return Ok(response);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("toggleArchive/{id}")]
        public async Task<IActionResult> ToggleArchiveDonationAppointment(long id)
        {
            var donation = await _appointmentsService.ToggleArchiveDonation(id);

            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("togglePublish/{id}")]
        public async Task<IActionResult> TogglePublishDonationAppointment(long id)
        {
            var donation = await _appointmentsService.TogglePublishDonation(id);

            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("reserve/{idDonation}/{idPatient}")]
        public async Task<IActionResult> ReserveDonation(long idDonation, long idPatient)
        {
            var donation = await _appointmentsService.ReserveDonation(idDonation, idPatient);

            if (donation == null)
            {
                return NotFound();
            }

            return Ok(donation);
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
        [HttpPut("reserve/{id1}/{id2}/{id3}")]
        public async Task<IActionResult> ReserveAppointment(long id1, long id2, long id3)
        {
            var appointment = await _appointmentsService.ReserveAppointmentAsync(id1, id2, id3);

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
