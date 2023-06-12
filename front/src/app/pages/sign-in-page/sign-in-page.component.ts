import { Component, OnInit } from '@angular/core';
import { LoginDTO } from '../../model/LoginDTO';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-sign-in-page',
  templateUrl: './sign-in-page.component.html',
  styleUrls: ['./sign-in-page.component.css'],
})
export class SignInPageComponent implements OnInit {
  private loginDTO: LoginDTO = new LoginDTO();

  constructor(private authorizationService: AuthorizationService) {}

  ngOnInit(): void {}

  onSubmit(): void {
    console.log(this.loginDTO);
    const isSuccess = this.authorizationService.login(this.loginDTO);
    if (isSuccess) {
      alert('Success!');
    } else {
      alert('Fail!');
    }
  }
}
