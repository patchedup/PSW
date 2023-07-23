import { Injectable } from '@angular/core';
import { InternistData } from '../model/InternistData';
import { Observable } from 'rxjs';
import { User } from '../model/User';
import { HttpClient } from '@angular/common/http';
import { CreateInternistDataDTO } from '../model/CreateInternistDataDTO';
import { Referral } from '../model/Referral';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  internistUrl: string = 'http://localhost:5098/api/InternistDatas';
  usersUrl: string = 'http://localhost:5098/api/users';
  referralsUrl: string = 'http://localhost:5098/api/referrals';

  constructor(private http: HttpClient) {}

  createInternistData(
    internistData: CreateInternistDataDTO
  ): Observable<InternistData> {
    return this.http.post<InternistData>(this.internistUrl, internistData);
  }

  getUserInternistData(): Observable<InternistData[]> {
    return this.http.get<InternistData[]>(`${this.internistUrl}/user-data`);
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl);
  }

  getAllReferrals(): Observable<Referral[]> {
    return this.http.get<Referral[]>(this.referralsUrl);
  }

  block(id: number): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/${id}/block`, {});
  }

  unblock(id: number): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/${id}/unblock`, {});
  }

  createReferral(referral: Referral): Observable<Referral> {
    return this.http.post<Referral>(this.referralsUrl, {
      forDoctorId: referral.forDoctorId,
      appointmentId: referral.appointments[0].id,
      isUsed: 0,
    });
  }
}
