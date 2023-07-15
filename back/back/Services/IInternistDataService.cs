using back.Dtos;
using back.Models;

namespace back.Services
{
    public interface IInternistDataService
    {
        Task<List<InternistData>> GetInternistDatasAsync();

        Task<InternistData> GetInternistDataByIdAsync(long id);

        Task<InternistData> UpdateInternistDataAsync(long id, InternistData internistData);

        Task<InternistData> CreateInternistDataAsync(InternistDataDTO internistData, long userId);

        Task<bool> DeleteInternistDataAsync(long id);

        Task<List<InternistData>> GetInternistDatasForUserAsync(long userId);
    }
}
