import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { OpenIdConnectService } from './open-id-connect.service';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class AddAuthorizationHeaderInterceptor implements HttpInterceptor {

    constructor(private openIdConnectService: OpenIdConnectService) {
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        req = req.clone({ setHeaders: { Authorization:
            this.openIdConnectService.user.token_type + ' ' + this.openIdConnectService.user.access_token }});

        const authorizationHeader = req.headers.getAll('Authorization');
        return next.handle(req);
    }
}
