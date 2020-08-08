import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { AuthService } from '../../auth/services/auth.service';
import { LoaderService } from '~/core/services';
import { Observable } from 'rxjs';
import { tap, finalize } from 'rxjs/operators';

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {

    constructor(private authService: AuthService, private loaderService: LoaderService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.showLoader();
        let authReq = req;
        // Get the auth token from the service.
        const authToken = this.authService.getToken();
        // Clone the request and replace the original headers with
        // cloned headers, updated with the authorization.
        if(authToken){
            authReq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + authToken)
            });
        }

        return next.handle(authReq)
            .pipe(tap((event: HttpEvent<any>) => {
                return event;                
            },
                (err: any) => {
                   
                }),
                    finalize(() => {
                    this.onEnd();
            }));
    }

    private onEnd(): void {
        this.hideLoader();
    }
    private showLoader(): void {
        this.loaderService.show();
    }
    private hideLoader(): void {
        this.loaderService.hide();
    }
}