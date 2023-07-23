import { Component, OnInit } from '@angular/core';
import { User } from '../../model/User';
import { UserService } from '../../services/user.service';
import { AuthorizationService } from '../../services/authorization.service';
import { Appointment } from '../../model/Appointement';
import { AppointmentService } from '../../services/appointment.service';
import { Referral } from '../../model/Referral';

@Component({
  selector: 'app-referral',
  templateUrl: './referral.component.html',
  styleUrls: ['./referral.component.css'],
})
export class ReferralComponent implements OnInit {
  doctors: User[] = [];
  selectedDoctor: number | null = null;
  appointments: Appointment[] = [];
  selectedAppointment: number | null = null;
  loggedInUser: User | null = null;
  referrals: Referral[] = [];

  constructor(
    private userService: UserService,
    private appointmentService: AppointmentService,
    private authService: AuthorizationService
  ) {
    this.loggedInUser = authService.getLoggedInUser();
    userService.getAllUsers().subscribe((result) => {
      this.doctors = result.filter((el) => el.role === 'DOCTOR');
    });

    userService.getAllReferrals().subscribe((result) => {
      this.referrals = result;
    });

    appointmentService.getAllAppointments().subscribe((result) => {
      this.appointments = result.filter(
        (el) => this.loggedInUser?.id && el.doctorId === this.loggedInUser?.id
      );
    });
  }

  onChange(newData: any): void {
    this.selectedAppointment = newData;
    console.log(this.selectedAppointment);
  }

  onChange2(newData: any): void {
    this.selectedDoctor = newData;
  }

  onSubmit(): void {
    if (!this.selectedDoctor || !this.selectedAppointment) {
      alert('You must select patient and doctor!');
      return;
    }
    this.userService
      .createReferral({
        isUsed: 0,
        forDoctorId: +this.selectedDoctor,
        forDoctor: null,
        // forDoctor:
        //   this.doctors.find((el) => el.id == this.selectedDoctor) ?? null,
        appointments: this.appointments.filter(
          (el) => el.id == this.selectedAppointment
        ),
      })
      .subscribe({
        next: (res) => {
          alert('Success!');
          // this.reports.unshift(res);
        },
        error: () => {
          alert('Fail!');
        },
      });
    // this.re.createReport(this.data).subscribe({
    //   next: (res) => {
    //     this.reports.unshift(res);
    //   },
    //   error: () => {
    //     alert('Fail!');
    //   },
    // });
  }

  ngOnInit(): void {}
}
