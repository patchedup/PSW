import { Component, OnInit } from '@angular/core';
import { InternistData } from '../../model/InternistData';
import { UserService } from '../../services/user.service';
import { AuthorizationService } from '../../services/authorization.service';
import { User } from '../../model/User';
import { CreateInternistDataDTO } from '../../model/CreateInternistDataDTO';

@Component({
  selector: 'app-internist-data',
  templateUrl: './internist-data.component.html',
  styleUrls: ['./internist-data.component.css'],
})
export class InternistDataComponent implements OnInit {
  data: CreateInternistDataDTO = new CreateInternistDataDTO();
  allDatas: InternistData[] = [];
  allMenstrualDatas: InternistData[] = [];
  loggedInUser: User | null = null;

  constructor(
    private userService: UserService,
    private authService: AuthorizationService
  ) {
    userService.getUserInternistData().subscribe((dat) => {
      this.allDatas = dat.filter(
        (el) => !el.menstruation_end_date && !el.menstruation_start_date
      );
      this.allMenstrualDatas = dat.filter(
        (el) => el.menstruation_end_date && el.menstruation_start_date
      );
      this.loggedInUser = authService.getLoggedInUser();
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    this.userService.createInternistData(this.data).subscribe({
      next: (result) => {
        console.log(result);
        this.allDatas.unshift(result);
        this.data = new CreateInternistDataDTO();
        alert('Successfully added measurement!');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onMenstrualSubmit(): void {
    if (!this.data.MenstruationEndDate || !this.data.MenstruationStartDate) {
      alert('You must fill both!');
      return;
    }

    this.userService.createInternistData(this.data).subscribe({
      next: (result) => {
        console.log(result);
        this.allMenstrualDatas.unshift(result);
        this.data = new CreateInternistDataDTO();
        alert('Successfully added measurement!');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
