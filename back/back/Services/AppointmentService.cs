using back.Dtos;
using back.Models;
using back.Protos;
using back.Repositories;
using Grpc.Core;
using System.Reflection.Metadata;

namespace back.Services
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepository _repository;
        IUsersService _usersService;
        IInternistDatasRepository _internistDatasRepository;
        IDonationRepository _donationRepository;

        public AppointmentService(IAppointmentRepository repository, IUsersService userService, IInternistDatasRepository internistDatasRepository, IDonationRepository donationRepository)
        {
            _usersService = userService;
            _repository = repository;
            _internistDatasRepository = internistDatasRepository;
            _donationRepository = donationRepository;
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

        public async Task<Donation> ToggleArchiveDonation(long id)
        {
            Donation? donation = await _donationRepository.GetDonationByIdAsync(id);
            if (donation == null)
            {
                throw new Exception();
            }

            donation.IsArchived = (ulong)(donation.IsArchived == 0 ? 1 : 0);
            donation.Is_Archived = (ulong)(donation.Is_Archived == 0 ? 1 : 0);

            var response = await _donationRepository.UpdateDonationAsync(donation);
            return response;
        }

        public async Task<Donation> TogglePublishDonation(long id)
        {
            Donation? donation = await _donationRepository.GetDonationByIdAsync(id);
            if (donation == null)
            {
                throw new Exception();
            }

            donation.ShouldPublish = (ulong)(donation.ShouldPublish == 0 ? 1 : 0);
            donation.Should_publish = (ulong)(donation.Should_publish == 0 ? 1 : 0);
            return await _donationRepository.UpdateDonationAsync(donation);
        }

        public async Task<Donation> ReserveDonation(long idDonation, long idPatient)
        {
            Donation? donation = await _donationRepository.GetDonationByIdAsync(idDonation);
            if (donation == null)
            {
                throw new Exception();
            }

            donation.PatientId = idPatient;
            donation.Patient = await _usersService.GetUserByIdAsync(idPatient);
            Channel channel = new Channel("127.0.0.1:8787", ChannelCredentials.Insecure);
            SpringGrpcService.SpringGrpcServiceClient client = new SpringGrpcService.SpringGrpcServiceClient(channel);
            // var response = await client.createAppointmentAsync(new FacilityIdRequest{ CenterId = 1});
            // save response to database
            // return response
            return await _donationRepository.UpdateDonationAsync(donation);
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

        public async Task<List<Donation>> GetDonationsAsync()
        {
            Channel channel = new Channel("127.0.0.1:8787", ChannelCredentials.Insecure);
            SpringGrpcService.SpringGrpcServiceClient client = new SpringGrpcService.SpringGrpcServiceClient(channel);
            // var response = await client.getAllAppointmentsAsync(new FacilityIdRequest{ CenterId = 1});
            // save response to database
            // return response
            return await _donationRepository.GetDonationsAsync();
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
