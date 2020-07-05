import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(req).pipe(
            catchError(e => {
                // tslint:disable-next-line: triple-equals
                if (e.status == 401) {
                    return throwError(e.statusText);
                }

                if (e instanceof HttpErrorResponse) {
                    const applicationError = e.headers.get('Application-Error');
                    if (applicationError) {
                        return throwError(applicationError);
                    }
                }
                const serverError = e.error;
                let modelStateError = '';
                if (serverError.errors && typeof serverError.errors === 'object') {
                    for (const key  in serverError.errors) {
                        if (serverError.errors[key]) {
                            modelStateError += serverError.errors[key] + '\n';
                        }
                    }
                }
                return throwError(modelStateError || serverError || 'Server error');
              }
            )
        );
    }
}

export const ErrorInterceptorPrivider = {
    provide : HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi : true
}