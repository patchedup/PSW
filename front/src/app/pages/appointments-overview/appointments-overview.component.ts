import { Component, OnInit } from '@angular/core';
import { Appointment } from '../../model/Appointement';
import { AppointmentService } from '../../services/appointment.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-appointments-overview',
  templateUrl: './appointments-overview.component.html',
  styleUrls: ['./appointments-overview.component.css'],
})
export class AppointmentsOverviewComponent implements OnInit {
  appointments: Appointment[] = [];

  constructor(
    private appointmentService: AppointmentService,
    private router: Router
  ) {
    appointmentService.getAllUserAppointments().subscribe((result) => {
      this.appointments = result;
    });
  }

  isDateInFuture(dateString: string) {
    const parts = dateString.split('/');
    const day = parseInt(parts[0], 10);
    const month = parseInt(parts[1], 10) - 1;
    const year = parseInt(parts[2], 10);
    const dateObject = new Date(year, month, day);

    // Get the current date
    const currentDate = new Date();

    // Compare the dates
    return dateObject > currentDate;
  }

  ngOnInit(): void {}

  cancel(id: number): void {
    this.appointmentService.cancel(id).subscribe({
      next: () => {
        alert('Successfully canceled!');
        this.router.navigate(['home']);
        this.appointments = this.appointments.filter((el) => {
          el.id !== id;
        });
      },
      error: () => {
        alert('You can cancel it maximum 2 days before it!');
      },
    });
  }
}
