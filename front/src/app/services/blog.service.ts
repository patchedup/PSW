import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Blog } from '../model/Blog';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  url = 'http://localhost:8080/api/blogs';

  constructor(private http: HttpClient) {}

  getAllBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(`${this.url}/Blogs`);
  }
}
