import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Notification } from '../model/Notification';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor() {}

  createNotification(notification: Notification): Observable<Notification> {
    return new Observable((observer) => {
      return observer.next(notification);
    });
  }

  getAllNotifications(): Observable<Notification[]> {
    return new Observable((observer) => {
      return observer.next([
        {
          id: 1,
          title: 'Not1',
          content: 'contendfndskjfn jfnskjf sdkf skjfdsdff',
        },
        {
          id: 2,
          title: 'Not2',
          content: 'oeern rjk lfsdf fff fsdnf lorep',
        },
      ]);
    });
  }
}
