using back.Models;

namespace back.Repositories
{
    public interface IReferralRepository
    {
        Task<List<Referral>> GetReferralAsync();
        Task<Referral?> GetReferralByIdAsync(long id);
        Task<Referral> CreateReferralAsync(Referral referral);
        Task<Referral> UpdateReferralAsync(Referral referral);
        Task<Referral> DeleteReferralAsync(Referral referral);
    }
}
