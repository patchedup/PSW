using back.Models;
using back.Repositories;

namespace back.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IReportRepository _reportsRepository;

        public ReportsService(IReportRepository reportsRepository)
        {
            _reportsRepository = reportsRepository;

        }

        public Task<Report> CreateReportAsync(Report report)
        {
            throw new NotImplementedException();
        }

        public Task<List<Report>> GetReportsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
