using back.Models;
using back.Repositories;
using back.Dtos;

namespace back.Services
{
    public class ReferralService : IReferralService
    {
        IReferralRepository _repository;
        IAppointmentRepository _appointmentsRepository;
        public ReferralService(IReferralRepository repository, IAppointmentRepository appointmentsRepository)
        {
            _appointmentsRepository  = appointmentsRepository;
            _repository = repository;
        }
        public async Task<Referral> CreateReferralAsync(ReferralDTO referral)
        {
            

            var reff = new Referral();
            reff.ForDoctorId = referral.ForDoctorId;
            reff.IsUsed = referral.IsUsed;

            var newBlog = await _repository.CreateReferralAsync(reff);

            var appointments = await _appointmentsRepository.GetAppointmentsAsync();

            foreach (Appointment a in appointments)
            {
                if (a.Id == referral.AppointmentId)
                {
                    a.ReferralId = newBlog.Id;
                    // mozda i report ceo da setujes

                    
                    await _appointmentsRepository.UpdateAppointmentAsync(a);
                    break;
                }
            }


            return newBlog;
        }

        // ne treba patient vrv, ili ako treba nzm
        public async Task<List<Referral>> GetReferralsAsync(long patientId)
        {

            var appointments = await _appointmentsRepository.GetAppointmentsAsync();
            var usersReferrals = new List<Referral>();

            foreach (Appointment a in appointments)
            {
                var refid = a.ReferralId;
                if (a.PatientId == patientId && refid != null)
                {
                    usersReferrals.Add(await _repository.GetReferralByIdAsync((long)refid));
                }
            }

            return usersReferrals;
        }
    }
}
