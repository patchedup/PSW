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

  // archive(appointmentId: number): Observable<BloodAppointment> {
  //   return this.http.put<BloodAppointment>(
  //     `${this.url}/archive/${appointmentId}`,
  //     {}
  //   );
  // }

  // publish(appointmentId: number): Observable<BloodAppointment> {
  //   return this.http.put<BloodAppointment>(
  //     `${this.url}/publish/${appointmentId}`,
  //     {}
  //   );
  // }

  archive(appointmentId: number): boolean {
    return true;
  }

  publish(appointmentId: number): boolean {
    return true;
  }

  reserve(appointmentId: number, patientId: number): boolean {
    return true;
  }

  getAllBloodAppointments(): BloodAppointment[] {
    return [
      {
        id: 1,
        hospitalName: 'hospital 1',
        isArchived: false,
        shouldPublish: true,
        time: '11/11/2023',
      },
      {
        id: 2,
        hospitalName: 'hospital 2',
        isArchived: false,
        shouldPublish: true,
        time: '10/10/2023',
      },
      {
        id: 3,
        hospitalName: 'hospital 3',
        isArchived: true,
        shouldPublish: true,
        time: '11/11/2023',
      },
      {
        id: 4,
        hospitalName: 'hospital 4',
        isArchived: false,
        shouldPublish: true,
        time: '21/12/2023',
      },
    ];
  }

  // getAllBloodAppointments(): Observable<BloodAppointment[]> {
  //   return this.http.get<BloodAppointment[]>(`${this.url}/blood-appointments`);
  // }
}
