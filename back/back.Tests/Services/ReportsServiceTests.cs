using back.Dtos;
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
        private readonly Mock<IInternistDatasRepository> _internistDataRepositoryMock;
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

        public ReportsServiceTests()
        {
            _reportRepositoryMock = new Mock<IReportRepository>();
            _internistDataRepositoryMock = new Mock<IInternistDatasRepository>();
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _reportsService = new ReportsService(_reportRepositoryMock.Object, _appointmentRepositoryMock.Object ,_internistDataRepositoryMock.Object);
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

            var reportDTO = new ReportDTO
            {
                Id = 1,
                Diagnosis = "DIA",
                Treatment = "TRI",
                AppointmentId = 1
            };

            var report = new Report
            {
                Id = 1,
                Diagnosis = "DIA",
                Treatment = "TRI"
            };


            _reportRepositoryMock.Setup(repository => repository.CreateReportAsync(It.IsAny<Report>())).ReturnsAsync(report);

            // Act
            var createdReport = await _reportsService.CreateReportAsync(reportDTO);

            // Assert
            Assert.Equal(reportDTO.Id, createdReport.Id);
            Assert.Equal(reportDTO.Treatment, createdReport.Treatment);
            Assert.Equal(reportDTO.Diagnosis, createdReport.Diagnosis);
        }

    }
}
