import { Injectable } from '@angular/core';
import { InternistData } from '../model/InternistData';
import { Observable } from 'rxjs';
import { User } from '../model/User';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  internistUrl : string = "http://localhost:5098/api/InternistDatas"
  constructor(private http : HttpClient) {}

  // mocked For now, until backend is implemented
  createInternistData(internistData: InternistData): Observable<InternistData> {
    return this.http.post<InternistData>(this.internistUrl, internistData);
  }

  getUserInternistData(): Observable<InternistData[]> {
    return this.http.get<InternistData[]>(`${this.internistUrl}/user-data`)
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
