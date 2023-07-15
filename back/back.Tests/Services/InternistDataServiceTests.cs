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
    public class InternistDataServiceTests
    {
        private readonly InternistDataService _internistDataService;
        private readonly Mock<IInternistDatasRepository> _internistDataRepositoryMock;
        private readonly Mock<IUsersService> _usersServiceMock;

        public InternistDataServiceTests()
        {
            _internistDataRepositoryMock = new Mock<IInternistDatasRepository>();
            _usersServiceMock = new Mock<IUsersService>();
            _internistDataService = new InternistDataService(_internistDataRepositoryMock.Object, _usersServiceMock.Object);
        }

        [Fact]
        public async Task CreateInternistDataAsync_ValidData_ReturnsNewInternistData()
        {
            // Arrange
            long userId = 1;
            var internistDataDto = new InternistDataDTO
            {
                BloodPressure = "120/80",
                BloodSugar = 80,
                BodyFat = 20,
                Weight = 70,
                MenstruationStartDate = "2022-01-01",
                MenstruationEndDate = "2022-01-05"
            };

            var user = new User { Id = userId };
            _usersServiceMock.Setup(service => service.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            var newInternistData = new InternistData
            {
                UserId = userId,
                BloodPressure = internistDataDto.BloodPressure,
                BloodSugar = internistDataDto.BloodSugar,
                BodyFat = internistDataDto.BodyFat,
                Weight = internistDataDto.Weight,
                Date = DateTime.UtcNow.ToShortDateString(),
                Menstruation_start_date = internistDataDto.MenstruationStartDate,
                Menstruation_end_date = internistDataDto.MenstruationEndDate
            };

            _internistDataRepositoryMock.Setup(repo => repo.CreateInternistDataAsync(It.IsAny<InternistData>()))
                .ReturnsAsync(newInternistData);

            _usersServiceMock.Setup(service => service.IsFemaleAsync(userId))
                .ReturnsAsync(true);

            // Act
            var result = await _internistDataService.CreateInternistDataAsync(internistDataDto, userId);

            // Assert
            Assert.Equal(newInternistData.UserId, result.UserId);
            Assert.Equal(newInternistData.BloodPressure, result.BloodPressure);
            Assert.Equal(newInternistData.BloodSugar, result.BloodSugar);
            Assert.Equal(newInternistData.BodyFat, result.BodyFat);
            Assert.Equal(newInternistData.Weight, result.Weight);
            Assert.Equal(newInternistData.Date, result.Date);
            Assert.Equal(newInternistData.Menstruation_start_date, result.Menstruation_start_date);
            Assert.Equal(newInternistData.Menstruation_end_date, result.Menstruation_end_date);
        }

        [Fact]
        public async Task GetInternistDatasForUserAsync_ValidUserId_ReturnsListOfInternistData()
        {
            // Arrange
            long userId = 1;

            var allInternistData = new List<InternistData>
            {
                new InternistData { Id = 1, UserId = userId },
                new InternistData { Id = 2, UserId = userId },
                new InternistData { Id = 3, UserId = 2 }
            };

            _internistDataRepositoryMock.Setup(repo => repo.GetInternistDatasAsync())
                .ReturnsAsync(allInternistData);

            // Act
            var result = await _internistDataService.GetInternistDatasForUserAsync(userId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(userId, result[0].UserId);
            Assert.Equal(userId, result[1].UserId);
        }
    }
}
