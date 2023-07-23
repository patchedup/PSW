import { Component, OnInit } from '@angular/core';
import { RecommendedParams } from '../../model/RecommendedParams';
import { User } from '../../model/User';
import { UserService } from '../../services/user.service';
import { AppointmentService } from '../../services/appointment.service';
import { Appointment } from '../../model/Appointement';
import { AuthorizationService } from '../../services/authorization.service';
import { Referral } from '../../model/Referral';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css'],
})
export class AppointmentsComponent implements OnInit {
  options: RecommendedParams = new RecommendedParams();
  doctors: User[] = [];
  isFirstTime: boolean = false;
  doctorId: number = 0;
  reccommended: Appointment | null = null;
  loggedUser: User | null = null;

  constructor(
    private userService: UserService,
    private authService: AuthorizationService,
    private appointmentsService: AppointmentService
  ) {
    let refs: Referral[] = [];
    this.loggedUser = this.authService.getLoggedInUser();
    this.appointmentsService
      .getAllUserAppointments()
      .subscribe((allPreviousAppointments) => {
        this.userService.getAllUsers().subscribe((res) => {
          const assignedDoctor = res.find(
            (el) => el.id === this.loggedUser?.assignedGeneralPracticeDoctorId
          );

          if (assignedDoctor) {
            this.doctors.push(assignedDoctor);
            this.doctorId = assignedDoctor.id;
          }

          this.userService.getAllReferrals().subscribe((usersReferrals) => {
            if (
              allPreviousAppointments.length === 0 &&
              usersReferrals.length === 0
            ) {
              this.isFirstTime = true;
            }
            console.log(allPreviousAppointments);
            console.log(usersReferrals);
            usersReferrals.forEach((refer) => {
              if (!refer.isUsed) {
                const doctorReferal = res.find(
                  (el) => el.id === refer.forDoctorId
                );
                // ako referal nije koriscen uvuci u doktore sve one koji imaju tu specijalizaciju a nisu vec u nizu
                res.forEach((doctor) => {
                  if (
                    doctor.specialization === doctorReferal?.specialization &&
                    !this.doctors.find((el) => el.id === doctor.id)
                  ) {
                    this.doctors.push(doctor);
                  }
                });
              }
            });
          });
        });
      });
  }

  ngOnInit(): void {}

  onChange(newId: any): void {
    this.doctorId = newId;
  }

  onSubmit(): void {
    this.options.doctorId = +this.doctorId;
    this.appointmentsService.getRecommended(this.options).subscribe({
      next: (res) => {
        this.reccommended = res;
      },
      error: () => {
        alert('Nothing found!');
      },
    });
  }

  reset(): void {
    this.reccommended = null;
    this.options = new RecommendedParams();
  }

  accept(): void {
    if (!this.reccommended) return;
    console.log(this.reccommended);
    this.appointmentsService.reserve(this.reccommended.id).subscribe({
      next: () => {
        this.reccommended = null;
        this.options = new RecommendedParams();
        alert('Success!');
      },
      error: () => {
        alert('Fail!');
      },
    });
  }
}
