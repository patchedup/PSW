using back.Models;

namespace back.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(long id);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
        Task<Appointment> DeleteAppointmentAsync(Appointment appointment);
    }
}
