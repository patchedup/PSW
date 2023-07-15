using back.Models;

namespace back.Services
{
    public interface IReportsService
    {

        Task<List<Report>> GetReportsAsync();

        Task<Report> CreateReportAsync(Report report);
    }
}
