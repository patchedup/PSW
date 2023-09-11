import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BloodAppointment } from '../model/BloodAppointment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BloodServiceService {
  url = 'http://localhost:5098/api/Appointments';

  constructor(private http: HttpClient) {}

  toggleArchive(appointmentId: number): Observable<BloodAppointment> {
    return this.http.put<BloodAppointment>(
      `${this.url}/toggleArchive/${appointmentId}`,
      {}
    );
  }

  togglePublish(appointmentId: number): Observable<BloodAppointment> {
    return this.http.put<BloodAppointment>(
      `${this.url}/togglePublish/${appointmentId}`,
      {}
    );
  }

  reserve(
    appointmentId: number,
    patientId: number
  ): Observable<BloodAppointment> {
    return this.http.put<BloodAppointment>(
      `${this.url}/reserve/${appointmentId}/${patientId}`,
      {}
    );
  }

  getAllBloodAppointments(): Observable<BloodAppointment[]> {
    return this.http.get<BloodAppointment[]>(`${this.url}/donations`);
  }
}
