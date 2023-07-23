import { Component, OnInit } from '@angular/core';
import { LoginDTO } from '../../model/LoginDTO';
import { AuthorizationService } from '../../services/authorization.service';
import { LoggedInUserDTO } from 'src/app/model/LoggedInUserDTO';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css'],
})
export class SignInPageComponent implements OnInit {
  loginDTO: LoginDTO = new LoginDTO();

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.authorizationService.login(this.loginDTO).subscribe({
      next: (loggedInUser: LoggedInUserDTO) => {
        localStorage.setItem('loggedInUser', JSON.stringify(loggedInUser.user));
        localStorage.setItem('jwtToken', loggedInUser.token);
        alert('Success!');
        this.router.navigate(['home']);
      },
      error: () => {
        alert('Failed to login, please check credentials and try again!');
      },
    });
  }
}
