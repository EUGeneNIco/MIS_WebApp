import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable, tap } from 'rxjs';
import { ApiCallStatusCodes } from '../_enums/enums';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const clonedReq = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + this.authService.authToken)
    });

    return next.handle(clonedReq).pipe(
      tap({
        next: (v) => {
          return next.handle(req.clone());
        },
        error: (e) => {
          // console.log('auth interceptor error', e);
          if (e.status == ApiCallStatusCodes.UNAUTHORIZED || e.status === 0) {
            console.log('Unauthorized: Navigated by Interceptor');

            this.authService.destroyAuthToken();

            this.router.navigateByUrl('/login');
          }
          else if (e.status == ApiCallStatusCodes.FORBIDDEN) {
            console.log('Navigated by Interceptor: 403 Status');

            this.router.navigateByUrl('pages/access');
          }
        }
      })
    );
  }
};
