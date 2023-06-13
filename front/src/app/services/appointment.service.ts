import { Injectable } from '@angular/core';
import { Appointment } from '../model/Appointement';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {
  constructor() {}

  // pass user id
  getAllUserAppointments(): Observable<Appointment[]> {
    return new Observable((observer) => {
      return observer.next([
        {
          id: 1,
          time: 'TODAY',
          internistData: {
            id: 1,
            bloodPressure: 10,
            bloodSuggar: '10',
            bodyFat: 10,
            measurmentDate: '',
            weight: 200,
          },
          doctorId: 1,
          patientId: 1,
        },
        {
          id: 2,
          time: 'TODAY',
          internistData: {
            id: 1,
            bloodPressure: 10,
            bloodSuggar: '10',
            bodyFat: 10,
            measurmentDate: '',
            weight: 200,
          },
          doctorId: 1,
          patientId: 1,
        },
      ]);
    });
  }
}
