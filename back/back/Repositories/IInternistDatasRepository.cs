using back.Models;

namespace back.Repositories
{
    public interface IInternistDatasRepository
    {
        Task<List<InternistData>> GetInternistDatasAsync();
        Task<InternistData?> GetInternistDataByIdAsync(long id);
        Task<InternistData> CreateInternistDataAsync(InternistData internistData);
        Task<InternistData> UpdateInternistDataAsync(InternistData internistData);
        Task<InternistData> DeleteInternistDataAsync(InternistData internistData);
    }
}
