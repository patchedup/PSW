using back.Dtos;
using back.Models;
using back.Repositories;
using System.Reflection.Metadata;

namespace back.Services
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _repository;
        IUsersService _usersService;
        IInternistDatasRepository _internistDatasRepository;

        public AppointmentService(IAppointmentRepository repository, IUsersService userService, IInternistDatasRepository internistDatasRepository)
        {
            _usersService = userService;
            _repository = repository;
            _internistDatasRepository = internistDatasRepository;
        }

        public async Task<Appointment> CancelAppointment(long id)
        {
            Appointment? appointment = await _repository.GetAppointmentByIdAsync(id);
           if (appointment == null)
            {
                throw new Exception();
            }

            var startDate = Convert.ToDateTime(appointment.Time);
            var twoDaysBefore = startDate.AddDays(-2);
            if(DateTime.Now >  twoDaysBefore)
            {
                throw new Exception();
            }


            appointment.Patient = null;
            appointment.PatientId = null;

            return await _repository.UpdateAppointmentAsync(appointment);
        }

        public async Task<Appointment> ReserveAppointmentAsync(long id, long patientId, long internistDataId)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(id);
            var patient = await _usersService.GetUserByIdAsync(patientId);
            var internistData = await _internistDatasRepository.GetInternistDataByIdAsync(internistDataId);

            if(appointment == null)
            {
                throw new Exception();
            }

            appointment.PatientId = patientId;
            appointment.Patient = patient;
            appointment.MeasuredInternistData = internistData;
            appointment.MeasuredInternistDataId =  internistDataId;

            return await _repository.UpdateAppointmentAsync(appointment);
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _repository.GetAppointmentsAsync();
        }

        public async Task<Appointment?> GetReccomendedAppointments(RecommendedParams recommendedParams, long patientId)
        {
            User patient = await _usersService.GetUserByIdAsync(patientId);
            User? doctor = null;
            if(patient!=null && patient.AppointmentPatients!= null && patient.AppointmentPatients.Count == 0 && patient.AssignedGeneralPracticeDoctorId != null)
            {
                doctor = await _usersService.GetUserByIdAsync((long)patient.AssignedGeneralPracticeDoctorId);
            } else
            {
                doctor = await _usersService.GetUserByIdAsync(recommendedParams.DoctorId);
            }

            List<Appointment> allAppointments = await this.GetAppointmentsAsync();
            var isDoctorPrio = recommendedParams.IsDoctorPriority;
            DateTime start = Convert.ToDateTime(recommendedParams.Start);
            DateTime end = Convert.ToDateTime(recommendedParams.End);
           

            foreach (Appointment appointment in allAppointments)
            {
                DateTime time = Convert.ToDateTime(appointment.Time);
                var isRangeOkay = time >= start && time < end;
                if (isRangeOkay && appointment.DoctorId == recommendedParams.DoctorId && appointment.PatientId == null)
                {
                    return appointment;
                }
            }

            if(isDoctorPrio)
            {
                foreach (Appointment appointment in allAppointments)
                {
                    DateTime time = Convert.ToDateTime(appointment.Time);
                    DateTime weekEarlier = start.AddDays(-7);
                    DateTime weekLater = end.AddDays(7);

                    var isRangeOkay = time >= weekEarlier && time < weekLater;
                    if (isRangeOkay && appointment.DoctorId == recommendedParams.DoctorId && appointment.PatientId == null)
                    {
                        return appointment;
                    }
                }
                return null;
            }

            var specialization = doctor.Specialization;
            // PRvi put na pregled
            if (patient != null && patient.AppointmentPatients != null && patient.AppointmentPatients.Count == 0 && patient.AssignedGeneralPracticeDoctorId != null)
            {
                return null;
            }

            foreach (Appointment appointment in allAppointments)
            {
                DateTime time = Convert.ToDateTime(appointment.Time);
                var isRangeOkay = time >= start && time < end;
                var appointmentDoctor = await _usersService.GetUserByIdAsync((long)appointment.DoctorId);
                var isSameSpecialization = appointmentDoctor != null && appointmentDoctor.Specialization == specialization;
                Console.WriteLine(isRangeOkay);
                Console.WriteLine(isSameSpecialization);
                Console.WriteLine(appointment);

                if (isRangeOkay && isSameSpecialization && appointment.PatientId == null)
                {
                    return appointment;
                }
            }

            return null;
            
        }

        public async Task<List<Appointment>> GetUsersAppointments(long id)
        {
            List<Appointment> appointments = await _repository.GetAppointmentsAsync();
            List<Appointment> filtered = new List<Appointment>();
            foreach (Appointment a in appointments)
            {
                if (a.PatientId == id)
                {
                    filtered.Add(a);
                }
            }

            return filtered;
        }
    }
}
