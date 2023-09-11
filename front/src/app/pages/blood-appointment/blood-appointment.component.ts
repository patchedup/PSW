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
  myBloodAppointments: BloodAppointment[] = [];

  constructor(
    private bloodAppointmentService: BloodServiceService,
    private router: Router,
    private authService: AuthorizationService,
    private userService: UserService
  ) {
    userService.getAllUsers().subscribe((result) => {
      this.patients = result.filter((el) => el.role === 'PATIENT');
    });

    this.loggedUser = authService.getLoggedInUser() || undefined;

    bloodAppointmentService.getAllBloodAppointments().subscribe((res) => {
      this.appointments = res.filter(
        (el) =>
          this.loggedUser?.role === 'ADMIN' ||
          (!el.isArchived && el.shouldPublish)
      );

      if (this.loggedUser?.role === 'PATIENT') {
        this.myBloodAppointments = this.appointments.filter(
          (el) => el.patientId === this.loggedUser?.id
        );
      }
    });
  }

  ngOnInit(): void {}

  archive(id: number): void {
    this.bloodAppointmentService.toggleArchive(id).subscribe(() => {
      this.appointments = this.appointments.map<BloodAppointment>((el) =>
        el.id === id ? { ...el, isArchived: !el.isArchived } : el
      );
    });
  }

  publish(id: number): void {
    this.bloodAppointmentService.togglePublish(id).subscribe(() => {
      this.appointments = this.appointments.map<BloodAppointment>((el) =>
        el.id === id ? { ...el, shouldPublish: !el.shouldPublish } : el
      );
    });
  }

  reserve(appointmentId: number): void {
    if (!this.selectedPatient) {
      return;
    }
    this.bloodAppointmentService
      .reserve(appointmentId, this.selectedPatient)
      .subscribe(() => {
        alert('Success!');
      });
  }

  onChange(newPatient: any): void {
    this.selectedPatient = newPatient;
  }
}
