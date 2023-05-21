import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css'],
})
export class NavigationBarComponent implements OnInit {
  constructor(public authorizationService: AuthorizationService) {}

  ngOnInit(): void {}
}
