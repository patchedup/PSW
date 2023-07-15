using back.Dtos;
using back.Models;
using back.Repositories;

namespace back.Services
{
    public class ReferralService : IReferralService
    {
        private readonly IReferralRepository _referralsRepository;

        public ReferralService(IReferralRepository referralsRepository)
        {
            _referralsRepository = referralsRepository;

        }

        public Task<Referral> CreateReferralAsync(ReferralDTO referral)
        {
            throw new NotImplementedException();
        }

        public Task<List<Referral>> GetReferralsAsync(long patientId)
        {
            throw new NotImplementedException();
        }
    }
}
