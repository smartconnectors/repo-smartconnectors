import { Inject, Injectable } from '@angular/core';
import { Observable, of, combineLatest } from 'rxjs';
import { includes } from 'lodash';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthService {
    private tokenName = 'token';

    constructor() {

    }

    public isAuthenticated(): Observable<boolean> {

        if (!this.hasValidToken()) {
            return of(false);
        }

        return of(true);
    }

    public hasValidToken(): Boolean {
        const token = localStorage.getItem(this.tokenName);

        return false;
    }

    public getToken(): string {
        if (this.hasValidToken) {
            return localStorage.getItem(this.tokenName);
        }

        return null;
    }

    public setToken(token: string): void {
        localStorage.setItem(this.tokenName, token);
    }
}
