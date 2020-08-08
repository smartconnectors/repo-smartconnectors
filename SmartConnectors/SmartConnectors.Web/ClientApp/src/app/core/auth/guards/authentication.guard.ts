import { Injectable, EventEmitter, OnDestroy } from '@angular/core';
import {
    Route,
    Router,
    CanActivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    CanLoad,
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationGuard implements OnDestroy, CanActivate, CanLoad {

    onAuthentication: EventEmitter<any>;
    subscription;

    constructor(private router: Router) {

        this.onAuthentication = new EventEmitter<any>();
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return this.isAuthenticated();
    }

    canLoad(route: Route) {
        return this.isAuthenticated();
    }

    isAuthenticated(): Observable<boolean> {
        // stubbed out method, in case more complicated logic needs to be added in guard itself
        return of(true);
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }
}
