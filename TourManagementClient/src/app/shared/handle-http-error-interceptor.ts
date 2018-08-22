import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HttpRequest, HttpHandler, HttpResponse, HttpEvent } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { GlobalErrorHandler } from './global-error-handler';


@Injectable()
export class HandleHttpErrorInterceptor implements HttpInterceptor {

    constructor(private errorHandler: GlobalErrorHandler) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError((error: HttpErrorResponse) => {
                if (error.status === 422) {
                    this.errorHandler.handleError(error);
                    return Observable.throw(error.error);
                } else {
                    return of(new HttpResponse());
                }
            }));
    }
}
