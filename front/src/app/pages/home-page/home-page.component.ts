import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../services/notification.service';
import { Notification } from '../../model/Notification';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  notifications: Notification[] = [];
  newNotification: Notification = new Notification();

  constructor(public notificationService: NotificationService) {
    notificationService.getAllNotifications().subscribe((result) => {
      console.log(result)
      this.notifications = result;
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    this.notificationService
      .createNotification(this.newNotification)
      .subscribe((result) => {
      
        this.notifications.unshift(result);
        this.newNotification = new Notification();
        alert('Success!');
      });
  }
}
