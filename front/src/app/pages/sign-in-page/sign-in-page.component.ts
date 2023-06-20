import { Component, OnInit } from '@angular/core';
import { LoginDTO } from '../../model/LoginDTO';
import { AuthorizationService } from '../../services/authorization.service';
import { LoggedInUserDTO } from 'src/app/model/LoggedInUserDTO';


@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css'],
})
export class SignInPageComponent implements OnInit {
  loginDTO: LoginDTO = new LoginDTO();

  constructor(private authorizationService: AuthorizationService) {}

  ngOnInit(): void {}

  onSubmit(): void {
    console.log(this.loginDTO);
    this.authorizationService.login(this.loginDTO).subscribe({
      next : (loggedInUser : LoggedInUserDTO) => {
        console.log(loggedInUser)
        localStorage.setItem('loggedInUser', JSON.stringify(loggedInUser.user));
        localStorage.setItem('jwtToken', loggedInUser.token);
        alert('Success!');
      },
      error : (err : any) => {
        console.log(err)
        alert('Fail!');
      }
    });
  }
}
