using back.Models;
using back.Repositories;
using back.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace back.Tests.Services
{
    public class ReportsServiceTests
    {
        private readonly ReportsService _reportsService;
        private readonly Mock<IReportRepository> _reportRepositoryMock;

        public ReportsServiceTests()
        {
            _reportRepositoryMock = new Mock<IReportRepository>();
            _reportsService = new ReportsService(_reportRepositoryMock.Object);
        }

        [Fact]
        public async Task GetReportsAsync_ValidRequest_ReturnsReports()
        {
            // Arrange
            var expectedReports = new List<Report> { new Report { Id = 1 }, new Report { Id = 2 } };
            _reportRepositoryMock.Setup(repository => repository.GetReportsAsync()).ReturnsAsync(expectedReports);

            // Act
            var reports = await _reportsService.GetReportsAsync();

            // Assert
            Assert.Equal(expectedReports, reports);
        }

        [Fact]
        public async Task CreateReportAsync_ValidReport_ReturnsCreatedReport()
        {
            // Arrange
            var report = new Report { Id = 1 };
            _reportRepositoryMock.Setup(repository => repository.CreateReportAsync(report)).ReturnsAsync(report);

            // Act
            var createdReport = await _reportsService.CreateReportAsync(report);

            // Assert
            Assert.Equal(report, createdReport);
        }

    }
}
