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
using System.Reflection.Metadata;
using System.Security.Claims;
using back.Dtos;

namespace back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralsController : ControllerBase
    {
        private readonly IReferralService _referralService;

        public ReferralsController(IReferralService referralService)
        {
            _referralService = referralService;
        }

        // GET: api/Referrals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referral>>> GetReferrals()
        {
            string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null)
            {
                return Problem("Entity set userIdString  is null.");
            }
            long userId = long.Parse(userIdString);

            var refs = await _referralService.GetReferralsAsync(userId);

            return refs;
        }

        // POST: api/Referrals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Referral>> PostReferral(ReferralDTO referral)
        {
            string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null)
            {
                return Problem("Entity set userIdString  is null.");
            }
            long userId = long.Parse(userIdString);

            var newBlog = await _referralService.CreateReferralAsync(referral);

            return CreatedAtAction("GetReferral", new { id = newBlog.Id }, newBlog);
        }
    }
}
