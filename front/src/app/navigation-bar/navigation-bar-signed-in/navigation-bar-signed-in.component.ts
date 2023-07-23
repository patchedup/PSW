import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../services/authorization.service';
import { Router } from '@angular/router';
import { User } from '../../model/User';

@Component({
  selector: 'app-navigation-bar-signed-in',
  templateUrl: './navigation-bar-signed-in.component.html',
  styleUrls: ['./navigation-bar-signed-in.component.css'],
})
export class NavigationBarSignedInComponent implements OnInit {
  loggedInUser: User | null = null;
  constructor(
    public authorizationService: AuthorizationService,
    private router: Router
  ) {
    this.loggedInUser = authorizationService.getLoggedInUser();
  }

  ngOnInit(): void {}

  logout(): void {
    this.authorizationService.logout();
    this.router.navigate(['signIn']);
  }
}
