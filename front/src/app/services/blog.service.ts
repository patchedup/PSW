import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blog } from '../model/Blog';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/User';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  url = 'http://localhost:5098/api/Blogs';

  constructor(private http: HttpClient) {}
  
  createBlog(blog: Blog): Observable<Blog> {
    return this.http.post<Blog>(this.url, blog);
  }

  getAllBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(this.url);
  }
}
