import { Component, OnInit } from '@angular/core';
import { MedicalReport } from '../../model/MedicalReport';
import { ReportService } from '../../services/report.service';
import { Appointment } from '../../model/Appointement';
import { AppointmentService } from '../../services/appointment.service';
import { AuthorizationService } from '../../services/authorization.service';
import { User } from '../../model/User';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css'],
})
export class ReportsComponent implements OnInit {
  reports: MedicalReport[] = [];
  selectedAppointment: Appointment | null = null;
  appointments: Appointment[] = [];
  data: MedicalReport = new MedicalReport();
  loggedInUser: User | null = null;

  constructor(
    private reportService: ReportService,
    private authService: AuthorizationService,
    private appointmentService: AppointmentService
  ) {
    this.loggedInUser = authService.getLoggedInUser();
    const loggedId = this.loggedInUser?.id;
    reportService.getAllReports().subscribe((result) => {
      if (this.loggedInUser?.role === 'PATIENT') {
        this.reports = result.filter(
          (el) => el.appointments?.[0]?.patientId === loggedId
        );
      } else {
        this.reports = result;
      }
    });
    appointmentService.getAllAppointments().subscribe((result) => {
      this.appointments = result.filter((el) => {
        return !!loggedId && el.doctorId === loggedId && el.reportId === null;
      });
    });
  }

  onChange(newData: any): void {
    this.selectedAppointment = newData;
  }

  onSubmit(): void {
    if (!this.selectedAppointment) {
      alert('You must select appointment!');
      return;
    }
    console.log;
    this.reportService
      .createReport({
        id: this.data.id,
        diagnosis: this.data.diagnosis,
        treatment: this.data.treatment,
        appointmentId: this.selectedAppointment,
      } as any as MedicalReport)
      .subscribe({
        next: (res) => {
          this.reportService.getAllReports().subscribe((datt) => {
            this.reports = datt;
          });
          alert('Reported added!');
          this.reports.unshift(res);
        },
        error: () => {
          alert('Fail!');
        },
      });
  }

  ngOnInit(): void {}
}
