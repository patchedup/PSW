import { Component, OnInit } from '@angular/core';
import { User } from '../../model/User';
import { AuthorizationService } from '../../services/authorization.service';
import { RegisterDTO } from 'src/app/model/RegisterDTO';

@Component({
  selector: 'app-sign-up-page',
  templateUrl: './sign-up-page.component.html',
  styleUrls: ['./sign-up-page.component.css'],
})
export class SignUpPageComponent implements OnInit {
  user: RegisterDTO = new RegisterDTO();

  constructor(private authorizationService: AuthorizationService) {}

  ngOnInit(): void {}

  onSubmit() {
    this.authorizationService.register(this.user).subscribe({
      
      next : (user) => {
        console.log(user)
        alert('Sucess!');
      },
      error : (err) => {
        console.log(err)
        alert("Fail!");
      }
    });
  
  }
}
