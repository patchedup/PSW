import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { InternistDataComponent } from './pages/internist-data/internist-data.component';
import { BlogsComponent } from './pages/blogs/blogs.component';
import { AdministrationComponent } from './pages/administration/administration.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { AppointmentsOverviewComponent } from './pages/appointments-overview/appointments-overview.component';
import { WriteReportComponent } from './pages/write-report/write-report.component';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'internist', component: InternistDataComponent },
  { path: 'blogs', component: BlogsComponent },
  { path: 'administration', component: AdministrationComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'write-report', component: WriteReportComponent },
  { path: 'appointment-history', component: AppointmentsOverviewComponent },
  { path: 'signIn', component: SignInPageComponent },
  { path: 'signUp', component: SignUpPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
