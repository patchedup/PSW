import { Component, OnInit } from '@angular/core';
import { User } from '../../model/User';
import { AuthorizationService } from '../../services/authorization.service';
import { RegisterDTO } from 'src/app/model/RegisterDTO';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css'],
})
export class SignUpPageComponent implements OnInit {
  user: RegisterDTO = new RegisterDTO();

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit() {
    this.user.is_female = this.user.is_female ? 1 : 0;
    this.authorizationService.register(this.user).subscribe({
      next: (user) => {
        console.log(user);
        alert(
          'You have successfully registered! You will be navigated to sign in page!'
        );
        this.router.navigate(['signIn']);
      },
      error: (err) => {
        console.log(err);
        alert('Failed to register!');
      },
    });
  }
}
