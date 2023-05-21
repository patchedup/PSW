import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-navigation-bar-signed-in',
  templateUrl: './navigation-bar-signed-in.component.html',
  styleUrls: ['./navigation-bar-signed-in.component.css'],
})
export class NavigationBarSignedInComponent implements OnInit {
  constructor(public authorizationService: AuthorizationService) {}

  ngOnInit(): void {}

  logout(): void {
    this.authorizationService.logout();
  }
}
