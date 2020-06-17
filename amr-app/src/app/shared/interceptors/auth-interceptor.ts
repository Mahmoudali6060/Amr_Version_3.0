import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { AuthService } from 'src/app/modules/authentication/services/auth.service';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private auth: AuthService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        let token = this.auth.getToken() // auth is provided via constructor.
        if (token) {
            // Logged in. Add Bearer token.
            return next.handle(
                req.clone({
                    headers: req.headers.append('Authorization', 'Bearer ' + token)
                })
            );
        }
        // Not logged in. Continue without modification.
        return next.handle(req);
    }
}
