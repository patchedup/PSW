import { Injectable } from '@angular/core';
import { User } from '../model/User';
import { LoginDTO } from '../model/LoginDTO';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  loggedInUser: User | null = new User();
  constructor() {}

  getLoggedInUser(): any {
    return this.loggedInUser;
  }

  register(user: User): boolean {
    return true; // Mocked for now, unitl real endpoint is implemented
  }

  login(loginDTO: LoginDTO): any {
    return true; // Mocked for now, unitl real endpoint is implemented
  }

  logout(): void {
    this.loggedInUser = null;
  }
}
