import { Component, OnInit } from '@angular/core';
import { MedicalReport } from '../../model/MedicalReport';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css'],
})
export class ReportsComponent implements OnInit {
  reports: MedicalReport[] = [];

  constructor(private reportService: ReportService) {
    reportService.getAllReports().subscribe((result) => {
      this.reports = result;
    });
  }

  ngOnInit(): void {}
}
