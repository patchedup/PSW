import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MedicalReport } from '../model/MedicalReport';
import { Appointment } from '../model/Appointement';
import { HttpClient } from '@angular/common/http';
import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  reportsUrl: string = 'http://localhost:5098/api/reports';

  constructor(private http: HttpClient, private auth: AuthorizationService) {}
  getAllReports(): Observable<MedicalReport[]> {
    return this.http.get<MedicalReport[]>(this.reportsUrl);
  }

  createReport(report: MedicalReport): Observable<MedicalReport> {
    console.log(report);
    return this.http.post<MedicalReport>(this.reportsUrl, report);
  }
}
