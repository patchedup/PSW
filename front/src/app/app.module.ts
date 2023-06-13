import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { NavigationBarSignedInComponent } from './navigation-bar/navigation-bar-signed-in/navigation-bar-signed-in.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';
import { FormsModule } from '@angular/forms';
import { InternistDataComponent } from './pages/internist-data/internist-data.component';
import { BlogsComponent } from './pages/blogs/blogs.component';
import { AdministrationComponent } from './pages/administration/administration.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { AppointmentsOverviewComponent } from './pages/appointments-overview/appointments-overview.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationBarComponent,
    NavigationBarSignedInComponent,
    HomePageComponent,
    SignUpPageComponent,
    SignInPageComponent,
    InternistDataComponent,
    BlogsComponent,
    AdministrationComponent,
    ReportsComponent,
    AppointmentsOverviewComponent,
  ],
  imports: [BrowserModule, HttpClientModule, AppRoutingModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
