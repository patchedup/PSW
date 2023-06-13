import { Component, OnInit } from '@angular/core';
import { Blog } from '../../model/Blog';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css'],
})
export class BlogsComponent implements OnInit {
  newBlog: Blog = new Blog();
  blogs: Blog[] = [];

  constructor(private blogService: BlogService) {
    blogService.getAllBlogs().subscribe((result) => {
      this.blogs = result;
    });
  }

  ngOnInit(): void {}

  onSubmit(): void {
    this.blogService.createBlog(this.newBlog).subscribe((result) => {
      this.blogs.unshift(result);
      this.newBlog = new Blog();
      alert('Success!');
    });
  }
}
