using back.Data;
using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly HospitalContext _context;

        public BlogRepository(HospitalContext context)
        {
            _context = context;

        }
        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> DeleteBlogAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog?> GetBlogByIdAsync(long id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> UpdateBlogAsync(Blog blog)
        {
            _context.Entry(blog).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
