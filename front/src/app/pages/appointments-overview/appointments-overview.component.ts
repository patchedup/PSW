import { Component, OnInit } from '@angular/core';
import { Appointment } from '../../model/Appointement';
import { AppointmentService } from '../../services/appointment.service';

@Component({
  selector: 'app-appointments-overview',
  templateUrl: './appointments-overview.component.html',
  styleUrls: ['./appointments-overview.component.css'],
})
export class AppointmentsOverviewComponent implements OnInit {
  appointments: Appointment[] = [];

  constructor(private appointmentService: AppointmentService) {
    appointmentService.getAllUserAppointments().subscribe((result) => {
      this.appointments = result;
    });
  }

  ngOnInit(): void {}
}
