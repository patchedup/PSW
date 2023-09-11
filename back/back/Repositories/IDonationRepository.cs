using back.Models;

namespace back.Repositories
{
    public interface IDonationRepository
    {
        Task<List<Donation>> GetDonationsAsync();

        Task<Donation?> GetDonationByIdAsync(long id);
        Task<Donation> UpdateDonationAsync(Donation donation);
    }
}
