using back.Data;
using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly HospitalContext _context;

        public ReportRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<Report> CreateReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report> DeleteReportAsync(Report report)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report?> GetReportByIdAsync(long id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }

        public async Task<Report> UpdateReportAsync(Report report)
        {
            _context.Entry(report).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return report;
        }
    }
}
