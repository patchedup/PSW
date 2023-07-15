using back.Data;
using back.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace back.Repositories
{
    public class ReferralRepository : IReferralRepository
    {
        private readonly HospitalContext _context;

        public ReferralRepository(HospitalContext context)
        {
            _context = context;

        }
        public async Task<Referral> CreateReferralAsync(Referral referral)
        {
            _context.Referrals.Add(referral);
            await _context.SaveChangesAsync();
            return referral;
        }

        public async Task<Referral> DeleteReferralAsync(Referral referral)
        {
            _context.Referrals.Remove(referral);
            await _context.SaveChangesAsync();
            return referral;
        }

        public async Task<List<Referral>> GetReferralAsync()
        {
            return await _context.Referrals.ToListAsync();
        }

        public async Task<Referral?> GetReferralByIdAsync(long id)
        {
            return await _context.Referrals.FindAsync(id);
        }

        public async Task<Referral> UpdateReferralAsync(Referral referral)
        {
            _context.Entry(referral).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return referral;
        }
    }
}
