import { Component, OnInit } from '@angular/core';
import { MedicalReport } from '../../model/MedicalReport';
import { ReportService } from '../../services/report.service';
import { AppointmentService } from '../../services/appointment.service';
import { Appointment } from '../../model/Appointement';

@Component({
  selector: 'app-write-report',
  templateUrl: './write-report.component.html',
  styleUrls: ['./write-report.component.css'],
})
export class WriteReportComponent implements OnInit {
  newReport: MedicalReport = new MedicalReport();
  id: string = ''; // appointment id
  appointmentsForReport: Appointment[] = [];

  constructor(
    private reportService: ReportService,
    private appointmentService: AppointmentService
  ) {
    appointmentService
      .getAllAppointmentsReadyForReport()
      .subscribe((result) => {
        this.appointmentsForReport = result;
      });
  }

  onChange(newId: any): void {
    this.id = newId;
  }

  ngOnInit(): void {}

  onSubmit(): void {}
}
