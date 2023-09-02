import { Component, OnInit } from '@angular/core';
import { Appointment } from '../../model/Appointement';
import { Router } from '@angular/router';
import { AppointmentService } from '../../services/appointment.service';
import { UserService } from '../../services/user.service';
import { AuthorizationService } from '../../services/authorization.service';
import { User } from '../../model/User';
import { BloodServiceService } from '../../services/blood-service.service';
import { BloodAppointment } from '../../model/BloodAppointment';

@Component({
  selector: 'app-blood-appointment',
  templateUrl: './blood-appointment.component.html',
  styleUrls: ['./blood-appointment.component.css'],
})
export class BloodAppointmentComponent implements OnInit {
  appointments: BloodAppointment[] = [];
  loggedUser?: User = new User();
  patients: User[] = [];
  selectedPatient: number | null = null;

  constructor(
    private bloodAppointmentService: BloodServiceService,
    private router: Router,
    private authService: AuthorizationService,
    private userService: UserService
  ) {
    // bloodAppointmentService.getAllBloodAppointments().subscribe((result) => {
    //   this.appointments = result.filter((el) => !el.isArchived);
    // });
    userService.getAllUsers().subscribe((result) => {
      this.patients = result.filter((el) => el.role === 'PATIENT');
    });

    this.loggedUser = authService.getLoggedInUser() || undefined;
    this.appointments = bloodAppointmentService
      .getAllBloodAppointments()
      .filter(
        (el) =>
          this.loggedUser?.role === 'ADMIN' ||
          (!el.isArchived && el.shouldPublish)
      );
    console.log(this.appointments);
  }

  ngOnInit(): void {}

  archive(id: number): void {
    this.bloodAppointmentService.archive(id);
  }

  publish(id: number): void {
    this.bloodAppointmentService.publish(id);
  }

  reserve(appointmentId: number): void {
    if (!this.selectedPatient) {
      return;
    }
    this.bloodAppointmentService.reserve(appointmentId, this.selectedPatient);
  }

  onChange(newPatient: any): void {
    this.selectedPatient = newPatient;
  }
}
