import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../model/User';

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.css'],
})
export class AdministrationComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService) {
    userService.getAllUsers().subscribe((result) => {
      this.users = result;
    });
  }

  ngOnInit(): void {}

  block(id: number): void {
    this.userService.block(id).subscribe((result) => {
      if (result) {
        this.users = this.users.map((el) => {
          if (el.id !== id) {
            return el;
          }

          return {
            ...el,
            isBlocked: true,
          };
        });

        alert('Success!');
      }
    });
  }

  unblock(id: number): void {
    this.userService.unblock(id).subscribe((result) => {
      if (result) {
        this.users = this.users.map((el) => {
          if (el.id !== id) {
            return el;
          }

          return {
            ...el,
            isBlocked: false,
          };
        });

        alert('Success!');
      }
    });
  }
}
