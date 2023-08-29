using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using back.Data;
using back.Models;
using back.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using back.Dtos;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternistDatasController : ControllerBase
    {
        private readonly IInternistDataService _internistDataService;

        public InternistDatasController(IInternistDataService internistDataService)
        {
            _internistDataService = internistDataService;
        }

        // GET: api/InternistDatums/5
        [Authorize]
        [HttpGet("user-data")]
        public async Task<ActionResult<IEnumerable<InternistData>>> GetInternistDataForUser()
        {
            string userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null)
            {
                return Problem("Entity set userIdString  is null.");
            }
            long userId = long.Parse(userIdString);
            var userInternestData = await _internistDataService.GetInternistDatasForUserAsync(userId);

            return Ok(userInternestData);
        }

        [HttpGet("users-data/{id}")]
        public async Task<ActionResult<IEnumerable<InternistData>>> GetInternistDataById(long id)
        {
            var datas = await _internistDataService.GetInternistDataByIdAsync(id);
            return Ok(datas);
        }

        // POST: api/InternistDatums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<InternistData>> PostInternistData(InternistDataDTO internistData)
        {
            string userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userIdString == null)
            {
                return Problem("Entity set userIdString  is null.");
            }
            long userId = long.Parse(userIdString);
            
            var newInternistData = await _internistDataService.CreateInternistDataAsync(internistData, userId);

            return CreatedAtAction(nameof(PostInternistData), newInternistData);
        }
    }
}
