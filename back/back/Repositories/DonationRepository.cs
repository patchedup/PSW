using back.Data;
using back.Models;
using Microsoft.EntityFrameworkCore;

namespace back.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly HospitalContext _context;

        public DonationRepository(HospitalContext context)
        {
            _context = context;
        }

        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _context.Donations.ToListAsync();
        }

        public async Task<Donation> UpdateDonationAsync(Donation donation)
        {
            
                _context.Entry(donation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return donation;
            
        }

        public async Task<Donation?> GetDonationByIdAsync(long id)
        {
            return await _context.Donations.FindAsync(id);
        }
    }
}
