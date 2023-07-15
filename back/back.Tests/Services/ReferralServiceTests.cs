using back.Dtos;
using back.Models;
using back.Repositories;
using back.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace back.Tests.Services
{
    public class ReferralServiceTests
    {
        private readonly ReferralService _referralService;
        private readonly Mock<IReferralRepository> _referralRepositoryMock;
        private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

        public ReferralServiceTests()
        {
            _referralRepositoryMock = new Mock<IReferralRepository>();
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _referralService = new ReferralService(_referralRepositoryMock.Object, _appointmentRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateReferralAsync_ValidReferral_ReturnsNewReferral()
        {
            // Arrange
            var referralDto = new ReferralDTO
            {
                ForDoctorId = 1,
                IsUsed = 1,
                AppointmentId = 2
            };

            var referral = new Referral
            {
                ForDoctorId = referralDto.ForDoctorId,
                IsUsed = referralDto.IsUsed
            };

            var appointment = new Appointment
            {
                Id = (long)referralDto.AppointmentId,
                ReferralId = null
            };

            _referralRepositoryMock.Setup(repo => repo.CreateReferralAsync(It.IsAny<Referral>()))
                .ReturnsAsync(referral);

            _appointmentRepositoryMock.Setup(repo => repo.GetAppointmentsAsync())
                .ReturnsAsync(new List<Appointment> { appointment });

            _appointmentRepositoryMock.Setup(repo => repo.UpdateAppointmentAsync(It.IsAny<Appointment>()))
                .ReturnsAsync(appointment);

            // Act
            var result = await _referralService.CreateReferralAsync(referralDto);

            // Assert
            Assert.Equal(referral.ForDoctorId, result.ForDoctorId);
            Assert.Equal(referral.IsUsed, result.IsUsed);
        }

        [Fact]
        public async Task GetReferralsAsync_ValidPatientId_ReturnsListOfReferrals()
        {
            // Arrange
            long patientId = 1;

            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, PatientId = patientId, ReferralId = 2 },
                new Appointment { Id = 2, PatientId = patientId, ReferralId = null },
                new Appointment { Id = 3, PatientId = 2, ReferralId = 3 }
            };

            var referrals = new List<Referral>
            {
                new Referral { Id = 2 },
                new Referral { Id = 4 },
                new Referral { Id = 3 }
            };

            _appointmentRepositoryMock.Setup(repo => repo.GetAppointmentsAsync())
                .ReturnsAsync(appointments);

            _referralRepositoryMock.Setup(repo => repo.GetReferralByIdAsync(It.IsAny<long>()))
                .ReturnsAsync((long id) => referrals.Find(r => r.Id == id));

            // Act
            var result = await _referralService.GetReferralsAsync(patientId);

            // Assert
            Assert.Equal(1, result.Count);
            Assert.Equal(2, result[0].Id);
        }
    }
}
