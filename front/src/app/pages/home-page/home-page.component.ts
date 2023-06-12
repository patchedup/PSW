import { Component, OnInit } from '@angular/core';
import { Blog } from '../../model/Blog';
import { User } from '../../model/User';
import { BlogService } from '../../services/blog.service';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent implements OnInit {
  blogs: Blog[] = [];
  newBlog: Blog = new Blog();
  loggedUser: User | null = null;

  constructor(
    public blogService: BlogService,
    public authenticationService: AuthorizationService
  ) {
    // blogService.getAllBlogs().subscribe((data) => {
    //   this.blogs = data;
    // });
    this.blogs = [
      {
        id: 1,
        content: 'jlsdnaskjd dskj asdkas d sajd as djaskdj askd',
        title: 'Blog title 1',
        doctor: new User(),
      },
      {
        id: 2,
        content: 'SLDKNAKJDNASKJDL  d sajd as djaskdj askd',
        title: 'Blog title 2',
        doctor: new User(),
      },
    ];
    this.loggedUser = this.authenticationService.getLoggedInUser();
    this.newBlog.doctor.id = +(this.loggedUser?.id || 0);
  }

  ngOnInit(): void {}

  create(): void {
    // this.contentService
    //   .createDailyMotivation(this.newMotivation)
    //   .subscribe((data) => {
    //     this.motivations = [...this.motivations, data];
    //   });
  }
}
