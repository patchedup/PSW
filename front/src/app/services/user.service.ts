import { Injectable } from '@angular/core';
import { InternistData } from '../model/InternistData';
import { Observable } from 'rxjs';
import { User } from '../model/User';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  internistUrl: string = 'http://localhost:5098/api/InternistDatas';
  usersUrl: string = 'http://localhost:5098/api/users';
  constructor(private http: HttpClient) {}

  // mocked For now, until backend is implemented
  createInternistData(internistData: InternistData): Observable<InternistData> {
    return this.http.post<InternistData>(this.internistUrl, internistData);
  }

  getUserInternistData(): Observable<InternistData[]> {
    return this.http.get<InternistData[]>(`${this.internistUrl}/user-data`);
  }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.usersUrl);
  }

  block(id: number): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/${id}/block`, {});
  }

  unblock(id: number): Observable<User> {
    return this.http.put<User>(`${this.usersUrl}/${id}/unblock`, {});
  }
}
