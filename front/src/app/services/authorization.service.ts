import { Injectable } from '@angular/core';
import { User } from '../model/User';
import { LoginDTO } from '../model/LoginDTO';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterDTO } from '../model/RegisterDTO';
import { LoggedInUserDTO } from '../model/LoggedInUserDTO';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  url: string = 'http://localhost:5098/api/auth';
  loggedInUser: User | null = new User();
  constructor(private http: HttpClient) {}

  getLoggedInUser(): User | null {
    const loggedInUserStr = localStorage.getItem('loggedInUser') ?? '';
    if (loggedInUserStr) {
      return JSON.parse(loggedInUserStr);
    }
    return null;
  }

  getJWTToken(): string {
    return localStorage.getItem('jwtToken') ?? '';
  }

  getUsers(): any {
    return this.http.get('http://localhost:5098/api/Users');
  }
  register(user: RegisterDTO): Observable<User> {
    return this.http.post<User>(`${this.url}/register`, user);
  }

  login(loginDTO: LoginDTO): Observable<LoggedInUserDTO> {
    return this.http.post<LoggedInUserDTO>(`${this.url}/login`, loginDTO);
  }

  logout(): void {
    localStorage.setItem('loggedInUser', JSON.stringify(null));
    localStorage.setItem('jwtToken', JSON.stringify(null));
    this.loggedInUser = null;
  }
}
