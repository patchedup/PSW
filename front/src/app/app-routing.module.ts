import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { SignInPageComponent } from './pages/sign-in-page/sign-in-page.component';
import { SignUpPageComponent } from './pages/sign-up-page/sign-up-page.component';
import { InternistDataComponent } from './pages/internist-data/internist-data.component';
import { BlogsComponent } from './pages/blogs/blogs.component';

const routes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'internist', component: InternistDataComponent },
  { path: 'blogs', component: BlogsComponent },
  { path: 'signIn', component: SignInPageComponent },
  { path: 'signUp', component: SignUpPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
