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
    public class AppointmentServiceTests
    {
        private readonly AppointmentService _appointmentService;
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;
        private readonly Mock<IInternistDatasRepository> _internistDataRepositoryMock;
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly Mock<IDonationRepository> _donationRepositoryMock;

        public AppointmentServiceTests()
        {
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _usersServiceMock = new Mock<IUsersService>();
            _internistDataRepositoryMock = new Mock<IInternistDatasRepository>();
            _donationRepositoryMock = new Mock<IDonationRepository>();
            _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object, _usersServiceMock.Object, _internistDataRepositoryMock.Object, _donationRepositoryMock.Object);
        }

        [Fact]
        public async Task CancelAppointment_ValidId_ReturnsCancelledAppointment()
        {
            // Arrange
            var appointmentId = 1;
            var appointment = new Appointment
            {
                Id = appointmentId,
                Patient = new User(),
                Time = DateTime.Now.AddDays(3).ToString()
            };

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync(appointment);

            _appointmentRepositoryMock.Setup(repository => repository.UpdateAppointmentAsync(appointment))
                .ReturnsAsync(appointment);

            // Act
            var result = await _appointmentService.CancelAppointment(appointmentId);

            // Assert
            Assert.Null(result.PatientId);
            Assert.Null(result.Patient);
        }

        [Fact]
        public async Task CancelAppointment_AppointmentNotFound_ThrowsException()
        {
            // Arrange
            var appointmentId = 1;

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync((Appointment)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _appointmentService.CancelAppointment(appointmentId));
        }

        [Fact]
        public async Task CancelAppointment_AppointmentWithinTwoDays_ThrowsException()
        {
            // Arrange
            var appointmentId = 1;
            var appointment = new Appointment
            {
                Id = appointmentId,
                Patient = new User(),
                Time = DateTime.Now.AddDays(1).ToString()
            };

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync(appointment);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _appointmentService.CancelAppointment(appointmentId));
        }

        [Fact]
        public async Task ReserveAppointmentAsync_ValidIdAndPatientId_ReturnsReservedAppointment()
        {
            // Arrange
            var appointmentId = 1;
            var patientId = 2;
            var appointment = new Appointment
            {
                Id = appointmentId,
                Patient = null,
                PatientId = null
            };
            var patient = new User
            {
                Id = patientId
            };

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync(appointment);

            _usersServiceMock.Setup(service => service.GetUserByIdAsync(patientId))
                .ReturnsAsync(patient);

            _appointmentRepositoryMock.Setup(repository => repository.UpdateAppointmentAsync(appointment))
                .ReturnsAsync(appointment);

            // Act
            var result = await _appointmentService.ReserveAppointmentAsync(appointmentId, patientId, 0);

            // Assert
            Assert.Equal(patientId, result.PatientId);
            Assert.Equal(patient, result.Patient);
        }

        [Fact]
        public async Task ReserveAppointmentAsync_AppointmentNotFound_ThrowsException()
        {
            // Arrange
            var appointmentId = 1;
            var patientId = 2;

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentByIdAsync(appointmentId))
                .ReturnsAsync((Appointment)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _appointmentService.ReserveAppointmentAsync(appointmentId, patientId, 0));
        }

        [Fact]
        public async Task GetAppointmentsAsync_ReturnsListOfAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment(),
                new Appointment(),
                new Appointment()
            };

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentsAsync())
                .ReturnsAsync(appointments);

            // Act
            var result = await _appointmentService.GetAppointmentsAsync();

            // Assert
            Assert.Equal(appointments, result);
        }

        [Fact]
        public async Task GetUsersAppointments_ValidId_ReturnsUserAppointments()
        {
            // Arrange
            var userId = 1;
            var appointments = new List<Appointment>
            {
                new Appointment { PatientId = userId },
                new Appointment { PatientId = 2 },
                new Appointment { PatientId = userId }
            };

            _appointmentRepositoryMock.Setup(repository => repository.GetAppointmentsAsync())
                .ReturnsAsync(appointments);

            // Act
            var result = await _appointmentService.GetUsersAppointments(userId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, appointment => Assert.Equal(userId, appointment.PatientId));
        }
    }
}
