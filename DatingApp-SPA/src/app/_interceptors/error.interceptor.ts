import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

 intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  return next.handle(req).pipe(
   catchError(error => {
    if (error instanceof HttpErrorResponse) {
     const applicationError = error.headers.get('Application-Error');
     if (applicationError) {
      return throwError(applicationError);
     }
    }
   })
  );
 }

}
