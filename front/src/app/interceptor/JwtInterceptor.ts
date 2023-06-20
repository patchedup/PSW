import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorizationService } from '../services/authorization.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    constructor(private authService : AuthorizationService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwtToken = this.authService.getJWTToken();
    const modifiedRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${jwtToken}`
      }
    });

    return next.handle(modifiedRequest);
  }
}