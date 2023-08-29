using back.Dtos;
using back.Models;

namespace back.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointmentsAsync();

        Task<Appointment?> GetReccomendedAppointments(RecommendedParams recommendedParams, long id);

        Task<List<Appointment>> GetUsersAppointments(long id);

        Task<Appointment> CancelAppointment(long id);

        Task<Appointment> ReserveAppointmentAsync(long id, long patientId, long internistDataId);
    }
}
