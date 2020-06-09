import { Injectable } from '@angular/core';
// import { ErrorDialogService } from '../error-dialog/errordialog.service';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor {
    count = 0;
    constructor(private spinner: NgxSpinnerService, private toastrService: ToastrService) {

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // this.spinner.show();
        const token: string = localStorage.getItem('token');

        if (token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
        }

        if (!request.headers.has('Content-Type')) {
            request = request.clone({ headers: request.headers.set('Content-Type', 'application/json') });
        }

        request = request.clone({ headers: request.headers.set('Accept', 'application/json') });

        // this.count++;
        return next.handle(request).pipe(
            map((event: HttpEvent<any>) => {
                if (event instanceof HttpResponse) {
                    this.toastrService.success("", "Success", { positionClass: 'toast-bottom-right' });

                    catchError((error: HttpErrorResponse) => {

                        let data = {};
                        data = {
                            reason: error && error.error.reason ? error.error.reason : '',
                            status: error.status
                        };
                        this.toastrService.error("", "Error !", { positionClass: 'toast-bottom-right' });

                        return throwError(error);
                    })
                }
                // this.count--;
                // if (this.count == 0) this.spinner.hide()

                return event;
            }));
    }
}