using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using back.Models;
using back.Services;
using Microsoft.AspNetCore.Authorization;

namespace back.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5/block
        [HttpPut("{id}/block")]
        public async Task<IActionResult> BlockUser(long id)
        {

            try
            {
                var result = await _userService.BlockUserAsync(id);
                return Ok(result);
            }
            catch (Exception) { return BadRequest(); }
        }

        // PUT: api/Users/5/unblock
        [HttpPut("{id}/unblock")]
        public async Task<IActionResult> UnblockUser(long id)
        {

            try
            {
                var result = await _userService.UnblockUserAsync(id);
                return Ok(result);
            }
            catch (Exception) { return BadRequest(); }
        }
    }
}