using back.Dtos;
using back.Models;
using back.Repositories;

namespace back.Services
{
    public class InternistDataService : IInternistDataService
    {
        private readonly IInternistDatasRepository _internistDataRepository;

        public InternistDataService(IInternistDatasRepository internistDataRepository)
        {
            _internistDataRepository = internistDataRepository;

        }

        public Task<InternistData> CreateInternistDataAsync(InternistDataDTO internistData, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInternistDataAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<InternistData> GetInternistDataByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<InternistData>> GetInternistDatasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<InternistData>> GetInternistDatasForUserAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<InternistData> UpdateInternistDataAsync(long id, InternistData internistData)
        {
            throw new NotImplementedException();
        }
    }
}
