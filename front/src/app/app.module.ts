import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { NavigationBarSignedInComponent } from './navigation-bar/navigation-bar-signed-in/navigation-bar-signed-in.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationBarComponent,
    NavigationBarSignedInComponent,
    HomePageComponent,
    SignUpPageComponent,
    SignInPageComponent,
  ],
  imports: [BrowserModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
