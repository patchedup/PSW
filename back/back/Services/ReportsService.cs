using back.Models;
using back.Repositories;

namespace back.Services
{
    public class ReportsService : IReportsService
    {
        IReportRepository _repository;

        public ReportsService(IReportRepository repository)
        {
            _repository = repository;
        }
        public async Task<Report> CreateReportAsync(Report report)
        {
            return await _repository.CreateReportAsync(report);

        }

        public async Task<List<Report>> GetReportsAsync()
        {
            return await _repository.GetReportsAsync();
        }
    }
}
