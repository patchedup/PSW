import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';
import { Notification } from '../../model/Notification';
import { AuthorizationService } from '../../services/authorization.service';
import { User } from '../../model/User';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  notifications: Notification[] = [];
  loggedInUser: User | null = null;
  newNotification: Notification = new Notification();

  constructor(
    public notificationService: NotificationService,
    public authService: AuthorizationService
  ) {
    notificationService.getAllNotifications().subscribe((result) => {
      console.log(result);
      this.notifications = result;
    });
    this.loggedInUser = authService.getLoggedInUser();
  }

  ngOnInit(): void {}

  onSubmit(): void {
    this.notificationService
      .createNotification(this.newNotification)
      .subscribe((result) => {
        this.notifications.unshift(result);
        this.newNotification = new Notification();
        alert('Notification added!');
      });
  }
}
