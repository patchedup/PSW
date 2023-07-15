using back.Data;
using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Repositories
{
    public class InternistDatasRepository : IInternistDatasRepository
    {
        private readonly HospitalContext _context;

        public InternistDatasRepository(HospitalContext context)
        {
            _context = context;
        }
        public async Task<InternistData> CreateInternistDataAsync(InternistData internistData)
        {
            _context.InternistData.Add(internistData);
            await _context.SaveChangesAsync();
            return internistData;
        }

        public async Task<InternistData> DeleteInternistDataAsync(InternistData internistData)
        {
            _context.InternistData.Remove(internistData);
            await _context.SaveChangesAsync();
            return internistData;
        }

        public async Task<InternistData?> GetInternistDataByIdAsync(long id)
        {
            return await _context.InternistData.FindAsync(id);
        }

        public async Task<List<InternistData>> GetInternistDatasAsync()
        {
            return await _context.InternistData.ToListAsync();
        }

        public async Task<InternistData> UpdateInternistDataAsync(InternistData internistData)
        {
            _context.Entry(internistData).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return internistData;
        }
    }
}
