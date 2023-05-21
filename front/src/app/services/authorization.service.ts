import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  loggedInUser: boolean = true;
  constructor() {}

  getLoggedInUser(): any {
    return this.loggedInUser;
  }

  logout(): void {
    this.loggedInUser = !this.loggedInUser;
  }
}
