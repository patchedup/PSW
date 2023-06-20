import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Notification } from '../model/Notification';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  url : string = 'http://localhost:5098/api/Notifications'
  constructor(private http : HttpClient) {}

  createNotification(notification: Notification): Observable<Notification> {
    return this.http.post<Notification>(this.url, notification);
  }

  getAllNotifications(): Observable<Notification[]> {
    return this.http.get<Notification[]>(this.url);
  }
}
