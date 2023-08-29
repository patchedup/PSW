using back.Dtos;
using back.Models;
using back.Repositories;

namespace back.Services
{
    public class ReportsService : IReportsService
    {
        IReportRepository _repository;
        IAppointmentRepository _appointmentRepository;
         IInternistDatasRepository _internistDatasRepository;

        public ReportsService(IReportRepository repository, IAppointmentRepository  appRepository, IInternistDatasRepository dataRepo )
        {
            _repository = repository;
            _appointmentRepository = appRepository;
             _internistDatasRepository = dataRepo;
        }
        public async Task<Report> CreateReportAsync(ReportDTO report2)
        {

            Report report = new Report();
            report.Diagnosis = report2.Diagnosis;
            report.Treatment = report2.Treatment;
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync( report2.AppointmentId );

            var createdReport = await _repository.CreateReportAsync(report);
     
            if(appointment != null)
            {
                appointment.ReportId = createdReport.Id;
                await _appointmentRepository.UpdateAppointmentAsync(appointment);
            }
            
            return createdReport;

        }

        public async Task<List<Report>> GetReportsAsync()
        {

             var reports = await _repository.GetReportsAsync();
             var appointments = await _appointmentRepository.GetAppointmentsAsync();
  
                foreach (var item in reports)
                {
                    if (appointments != null)
                    {
                        foreach (var a in appointments)
                        {
                            if (a.ReportId == item.Id)
                            {
                                item.Appointments.Add(a);
                            }
                        }
                    }

                
            }
          
             return reports;
        }
    }
}
