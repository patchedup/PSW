using back.Dtos;
using back.Models;
using back.Repositories;
using System.Security.Claims;

namespace back.Services
{
    public class InternistDataService : IInternistDataService
    {
        private readonly IInternistDatasRepository _internistDataRepository;
        private readonly IUsersService _usersService;

        public InternistDataService(IInternistDatasRepository internistDataRepository, IUsersService usersService)
        {
            _internistDataRepository = internistDataRepository;
            _usersService = usersService;
        }

        public async Task<InternistData> CreateInternistDataAsync(InternistDataDTO internistData, long userId)
        {
            var newInternistData = new InternistData();
            var user = await _usersService.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("ERROR");
            }
            newInternistData.UserId = userId;
            newInternistData.BloodPressure = internistData.BloodPressure;
            newInternistData.Date = DateTime.UtcNow.ToShortDateString();
            newInternistData.Weight = internistData.Weight;
            newInternistData.BloodSugar = internistData.BloodSugar;
            newInternistData.BodyFat = internistData.BodyFat;

            var isFemale = await _usersService.IsFemaleAsync(userId);
            if (isFemale == true)
            {
                newInternistData.Menstruation_start_date = internistData.MenstruationStartDate;
                newInternistData.Menstruation_end_date = internistData.MenstruationEndDate;
            }

            return await _internistDataRepository.CreateInternistDataAsync(newInternistData);
        }

        public Task<bool> DeleteInternistDataAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<InternistData> GetInternistDataByIdAsync(long id)
        {
            return await _internistDataRepository.GetInternistDataByIdAsync(id);
        }

        public Task<List<InternistData>> GetInternistDatasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<InternistData> UpdateInternistDataAsync(long id, InternistData internistData)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InternistData>> GetInternistDatasForUserAsync(long userId)
        {
            var allData = await _internistDataRepository.GetInternistDatasAsync();
            var userInternistData = new List<InternistData>();
            foreach (var item in allData)
            {
                if(item.UserId == userId)
                {
                    userInternistData.Add(item);
                }
            }

            return userInternistData;

        }
    }
}
