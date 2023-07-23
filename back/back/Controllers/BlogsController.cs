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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace back.Controllers
{
    [Route("api/Blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsService _blogService;

        public BlogsController(IBlogsService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var blogs = await _blogService.GetBlogsAsync();
          if (blogs == null)
          {
              return NotFound();
          }
            return blogs;
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "DoctorPolicy")]
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
            string? userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdString == null)
            {
                return Problem("Entity set userIdString  is null.");
            }
            long userId = long.Parse(userIdString);

            var newBlog = await _blogService.CreateBlogAsync(blog, userId);

            return CreatedAtAction(nameof(PostBlog), new { id = newBlog.Id }, newBlog);
        }
    }
}
