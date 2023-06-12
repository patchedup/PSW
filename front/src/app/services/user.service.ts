import { Injectable } from '@angular/core';
import { InternistData } from '../model/InternistData';
import { Observable } from 'rxjs';

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
}
