import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalReport } from '../model/MedicalReport';
import { Appointment } from '../model/Appointement';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  constructor() {}

  getAllReports(): Observable<MedicalReport[]> {
    return new Observable((observer) => {
      return observer.next([
        {
          id: 1,
          diagnosis: 'Common cold',
          treatment: 'Lay down and chill',
          appointment: { ...new Appointment(), time: 'TODAY' },
        },
        {
          id: 2,
          diagnosis: 'Broken leg',
          treatment: 'stay still',
          appointment: { ...new Appointment(), time: 'YESTERDAY' },
        },
      ]);
    });
  }

  // mocked For now, until backend is implemented
  createReport(report: MedicalReport): Observable<MedicalReport> {
    return new Observable((observer) => {
      return observer.next(report);
    });
  }
}
