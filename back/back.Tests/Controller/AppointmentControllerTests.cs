using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using back.Controllers;
using back.Models;
using back.Dtos;
using back.Services;

namespace back.Tests.Controllers
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _appointmentServiceMock;
        private readonly AppointmentsController _appointmentsController;

        public AppointmentsControllerTests()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>();
            _appointmentsController = new AppointmentsController(_appointmentServiceMock.Object);
        }

        [Fact]
        public async Task GetAppointments_WithExistingAppointments_ReturnsOkResultWithAppointments()
        {
            // Arrange
            var appointments = new List<Appointment> { new Appointment(), new Appointment() };
            _appointmentServiceMock.Setup(service => service.GetAppointmentsAsync()).ReturnsAsync(appointments);

            // Act
            var result = await _appointmentsController.GetAppointments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointments = Assert.IsAssignableFrom<IEnumerable<Appointment>>(okResult.Value);
            Assert.Equal(appointments.Count, returnedAppointments.Count());
            Assert.Equal(appointments, returnedAppointments);
        }

        [Fact]
        public async Task GetAppointments_NoAppointmentsExist_ReturnsNotFoundResult()
        {
            // Arrange
            _appointmentServiceMock.Setup(service => service.GetAppointmentsAsync()).ReturnsAsync((List<Appointment>)null);

            // Act
            var result = await _appointmentsController.GetAppointments();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetRecommendedAppointment_InvalidParams_ReturnsNotFoundResult()
        {
            // Arrange
            var id = 1L;
            var recommendedParams = new RecommendedParams(true, 2L, "2023-07-15", "2023-07-16");

            _appointmentServiceMock.Setup(service => service.GetReccomendedAppointments(recommendedParams, id)).ReturnsAsync((Appointment)null);

            // Act
            var result = await _appointmentsController.GetRecommendedAppointment(id, recommendedParams);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task ReserveAppointment_ValidIds_ReturnsOkResultWithReservedAppointment()
        {
            // Arrange
            var id1 = 1L;
            var id2 = 2L;
            var reservedAppointment = new Appointment { Id = 1 };

            _appointmentServiceMock.Setup(service => service.ReserveAppointmentAsync(id1, id2, 0)).ReturnsAsync(reservedAppointment);

            // Act
            var result = await _appointmentsController.ReserveAppointment(id1, id2, 0);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAppointment = Assert.IsAssignableFrom<Appointment>(okResult.Value);
            Assert.Equal(reservedAppointment, returnedAppointment);
        }

        [Fact]
        public async Task ReserveAppointment_InvalidIds_ReturnsNotFoundResult()
        {
            // Arrange
            var id1 = 1L;
            var id2 = 2L;

            _appointmentServiceMock.Setup(service => service.ReserveAppointmentAsync(id1, id2, 0)).ReturnsAsync((Appointment)null);

            // Act
            var result = await _appointmentsController.ReserveAppointment(id1, id2, 0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CancelAppointment_ValidId_ReturnsOkResultWithCancelledAppointment()
        {
            // Arrange
            var id = 1L;
            var cancelledAppointment = new Appointment { Id = 1 };

            _appointmentServiceMock.Setup(service => service.CancelAppointment(id)).ReturnsAsync(cancelledAppointment);

            // Act
            var result = await _appointmentsController.CancelAppointment(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAppointment = Assert.IsAssignableFrom<Appointment>(okResult.Value);
            Assert.Equal(cancelledAppointment, returnedAppointment);
        }

        [Fact]
        public async Task CancelAppointment_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var id = 1L;

            _appointmentServiceMock.Setup(service => service.CancelAppointment(id)).ReturnsAsync((Appointment)null);

            // Act
            var result = await _appointmentsController.CancelAppointment(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
