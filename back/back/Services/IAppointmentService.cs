using back.Dtos;
using back.Models;

namespace back.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointmentsAsync();

        Task<List<Donation>> GetDonationsAsync();

        Task<Appointment?> GetReccomendedAppointments(RecommendedParams recommendedParams, long id);

        Task<List<Appointment>> GetUsersAppointments(long id);

        Task<Appointment> CancelAppointment(long id);

        Task<Donation> ToggleArchiveDonation(long id);

        Task<Donation> TogglePublishDonation(long id);

        Task<Donation> ReserveDonation(long idDonation, long idPatient);

        Task<Appointment> ReserveAppointmentAsync(long id, long patientId, long internistDataId);
    }
}
