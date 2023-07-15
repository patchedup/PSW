using back.Dtos;
using back.Models;

namespace back.Services
{
    public interface IReferralService
    {
        Task<List<Referral>> GetReferralsAsync(long patientId);

        Task<Referral> CreateReferralAsync(ReferralDTO referral);
    }
}

