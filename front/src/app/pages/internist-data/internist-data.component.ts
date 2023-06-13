import { Component, OnInit } from '@angular/core';
import { InternistData } from '../../model/InternistData';
import { UserService } from '../../services/user.service';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-internist-data',
  templateUrl: './internist-data.component.html',
  styleUrls: ['./internist-data.component.css'],
})
export class InternistDataComponent implements OnInit {
  data: InternistData = new InternistData();
  allDatas: InternistData[] = [];

  constructor(private userService: UserService) {
    userService.getUserInternistData().subscribe((dat) => {
      this.allDatas = dat;
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    this.userService.createInternistData(this.data).subscribe((result) => {
      this.allDatas.unshift(result);
      this.data = new InternistData();
      alert('Success!');
    });
  }
}
