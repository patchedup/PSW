using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using back.Controllers;
using back.Models;
using back.Services;
using back.Dtos;

namespace back.Tests.Controllers
{
    public class ReportsControllerTests
    {
        private readonly Mock<IReportsService> _reportsServiceMock;
        private readonly ReportsController _reportsController;

        public ReportsControllerTests()
        {
            _reportsServiceMock = new Mock<IReportsService>();
            _reportsController = new ReportsController(_reportsServiceMock.Object);
        }

        [Fact]
        public async Task GetReports_ReturnsOkResultWithReports()
        {
            // Arrange
            var reports = new List<Report> { new Report(), new Report() };
            _reportsServiceMock.Setup(service => service.GetReportsAsync()).ReturnsAsync(reports);

            // Act
            var result = await _reportsController.GetReports();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedReports = Assert.IsAssignableFrom<IEnumerable<Report>>(okResult.Value);
            Assert.Equal(reports.Count, returnedReports.Count());
            Assert.Equal(reports, returnedReports);
        }

        [Fact]
        public async Task PostReport_ValidReport_ReturnsOkResultWithCreatedReport()
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

            _reportsServiceMock.Setup(service => service.CreateReportAsync(reportDTO)).ReturnsAsync(report);

            // Act
            var result = await _reportsController.PostReport(reportDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedReport = Assert.IsAssignableFrom<Report>(okResult.Value);
            Assert.Equal(reportDTO.Id, returnedReport.Id);
            Assert.Equal(reportDTO.Treatment, returnedReport.Treatment);
            Assert.Equal(reportDTO.Diagnosis, returnedReport.Diagnosis);
        }

        [Fact]
        public async Task PostReport_InvalidReport_ReturnsBadRequestResult()
        {
            // Arrange
            var reportDTO = new ReportDTO
            {
                Id = 1,
                Diagnosis = "DIA",
                Treatment = "TRI",
                AppointmentId = 1
            };

            _reportsServiceMock.Setup(service => service.CreateReportAsync(reportDTO)).ThrowsAsync(new Exception());

            // Act
            var result = await _reportsController.PostReport(reportDTO);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}
