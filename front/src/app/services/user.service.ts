import { Injectable } from '@angular/core';
import { InternistData } from '../model/InternistData';
import { Observable } from 'rxjs';
import { User } from '../model/User';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor() {}

  // mocked For now, until backend is implemented
  createInternistData(internistData: InternistData): Observable<InternistData> {
    return new Observable((observer) => {
      return observer.next(internistData);
    });
  }

  getUserInternistData(): Observable<InternistData[]> {
    return new Observable((observer) => {
      return observer.next([
        {
          id: 1,
          bloodPressure: 1,
          bloodSuggar: '1',
          bodyFat: 1,
          weight: 1,
          measurmentDate: 'Today',
        },
        {
          id: 2,
          bloodPressure: 2,
          bloodSuggar: '2',
          bodyFat: 2,
          weight: 2,
          measurmentDate: 'Yesterday',
        },
      ]);
    });
  }

  getAllUsers(): Observable<User[]> {
    return new Observable((observer) => {
      return observer.next([
        {
          id: 1,
          firstName: 'FN1',
          lastName: 'LN1',
          email: '1@gmail.com',
          isBlocked: false,
          numberOfPenalties: 2,
          password: '',
          role: 'PATIENT',
          assignedGeneralPracticeId: 0,
          specialization: '',
        },
        {
          id: 2,
          firstName: 'FN2',
          lastName: 'LN2',
          email: '2@gmail.com',
          isBlocked: true,
          numberOfPenalties: 3,
          password: '',
          role: 'PATIENT',
          assignedGeneralPracticeId: 0,
          specialization: '',
        },
        {
          id: 3,
          firstName: 'FN3',
          lastName: 'LN3',
          email: '3@gmail.com',
          isBlocked: false,
          numberOfPenalties: 3,
          password: '',
          role: 'PATIENT',
          assignedGeneralPracticeId: 0,
          specialization: '',
        },
      ]);
    });
  }

  block(id: number): Observable<true> {
    return new Observable((observer) => {
      observer.next(true);
    });
  }

  unblock(id: number): Observable<true> {
    return new Observable((observer) => {
      observer.next(true);
    });
  }
}
