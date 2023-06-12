import { Component, OnInit } from '@angular/core';
import { User } from '../../model/User';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css'],
})
export class SignUpPageComponent implements OnInit {
  user: User = new User();

  constructor(private authorizationService: AuthorizationService) {}

  ngOnInit(): void {}

  onSubmit() {
    console.log(this.user);
    const isSuccess = this.authorizationService.register(this.user);
    if (isSuccess) {
      alert('Sucess!');
    } else {
      alert('Fail!');
    }
  }
}
