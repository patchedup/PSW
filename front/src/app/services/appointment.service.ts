import { Injectable } from '@angular/core';
import { Appointment } from '../model/Appointement';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RecommendedParams } from '../model/RecommendedParams';
import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  appointmentsUrl: string = 'http://localhost:5098/api/appointments';

  constructor(private http: HttpClient, private auth: AuthorizationService) {}

  getRecommended(options: RecommendedParams): Observable<Appointment> {
    return this.http.post<Appointment>(
      `${this.appointmentsUrl}/${
        this.auth.getLoggedInUser()?.id || ''
      }/get-recommended`,
      options
    );
  }

  reserve(appointmentId: number, dataId: number): Observable<Appointment> {
    return this.http.put<Appointment>(
      `${this.appointmentsUrl}/reserve/${appointmentId}/${
        this.auth.getLoggedInUser()?.id || ''
      }/${dataId}`,
      {}
    );
  }

  cancel(appointmentId: number): Observable<Appointment> {
    return this.http.put<Appointment>(
      `${this.appointmentsUrl}/cancel/${appointmentId}`,
      {}
    );
  }

  // pass user id
  getAllUserAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(
      `${this.appointmentsUrl}/users-appointments/${
        this.auth.getLoggedInUser()?.id || ''
      }`
    );
  }

  getAllAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.appointmentsUrl);
  }
}
