import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blog } from '../model/Blog';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/User';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  url = 'http://localhost:8080/api/blogs';

  constructor(private http: HttpClient) {}

  // getAllBlogs(): Observable<Blog[]> {
  //   return this.http.get<Blog[]>(`${this.url}/Blogs`);
  // }

  // mocked For now, until backend is implemented
  createBlog(blog: Blog): Observable<Blog> {
    return new Observable((observer) => {
      return observer.next(blog);
    });
  }

  getAllBlogs(): Observable<Blog[]> {
    return new Observable((observer) => {
      return observer.next([
        {
          id: 1,
          content: 'some content 1',
          title: 'some title 1',
          doctor: new User(),
        },
        {
          id: 2,
          content: 'some content 2',
          title: 'some title 2',
          doctor: new User(),
        },
      ]);
    });
  }
}
