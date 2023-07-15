using back.Dtos;
using back.Models;
using back.Repositories;

namespace back.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentsRepository;

        public AppointmentService(IAppointmentRepository appointmentsRepository)
        {
            _appointmentsRepository = appointmentsRepository;

        }

        public Task<Appointment> CancelAppointment(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAppointmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment?> GetReccomendedAppointments(RecommendedParams recommendedParams, long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetUsersAppointments(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> ReserveAppointmentAsync(long id, long patientId)
        {
            throw new NotImplementedException();
        }
    }
}
