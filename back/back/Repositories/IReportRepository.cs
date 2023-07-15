using back.Models;

namespace back.Repositories
{
    public interface IReportRepository
    {
        Task<List<Report>> GetReportsAsync();
        Task<Report?> GetReportByIdAsync(long id);
        Task<Report> CreateReportAsync(Report report);
        Task<Report> UpdateReportAsync(Report report);
        Task<Report> DeleteReportAsync(Report report);
    }
}
