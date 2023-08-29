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

  selectedDate: Date[] = [];
  selectedDate2: Date | null = null;
  onSelect(event: any) {
    console.log(event);
    this.selectedDate2 = event;
  }
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
      this.selectedDate = this.allMenstrualDatas.reduce<Date[]>((acc, curr) => {
        if (curr.menstruation_start_date)
          return [...acc, this.parseDateString(curr.menstruation_start_date)];

        return acc;
      }, []);

      this.loggedInUser = authService.getLoggedInUser();
    });
  }

  ngOnInit(): void {}

  private parseDateString(dateString: string): Date {
    const parts = dateString.split('/');
    const day = parseInt(parts[0], 10);
    const month = parseInt(parts[1], 10) - 1; // Months are zero-based
    const year = parseInt(parts[2], 10);
    return new Date(year, month, day);
  }

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
