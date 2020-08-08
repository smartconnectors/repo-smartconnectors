import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, of, throwError, Subject } from 'rxjs';
import { tap, catchError, map, timeout, flatMap } from 'rxjs/operators';
import { CacheService } from '../../cache/services/cache.service';
import { AuthService } from '../../auth/services/auth.service';

export interface HttpOptions {
    headers?: HttpHeaders | {
        [header: string]: string | string[];
    };

    params?: HttpParams | {
        [param: string]: string | string[];
    };
}

@Injectable({
    providedIn: 'root'
})

export class HttpService {
    subject: Subject<any> = new Subject();
    constructor(public http: HttpClient, public cache: CacheService, private authService: AuthService) { }

    get<T = any>(url: string, skipCache: boolean = false, options?: HttpOptions, forceGetFromHttp: boolean = false): Observable<T> {
        let queryString = '';

        if (options && options.params) {
            queryString = '?' + Object.keys(options.params).map(key => key + '=' + options.params[key]).join('&');
        }

        if (!forceGetFromHttp && !skipCache && this.cache.has(url + queryString)) {
            return of(this.cache.get(url + queryString));
        }

        return this.http.get<T>(url, options)
            .pipe(tap(this.placeInCache<T>(url + queryString, !skipCache)))
            .pipe(catchError(this.handleError));
    }

    put<T = any, R = T>(url: string, input: T, useCache: boolean = false, options?: HttpOptions): Observable<R> {
        if (useCache && this.cache.has(url)) {
            return of(this.cache.get(url));
        }

        return this.http.put<R>(url, input, options);
    }

    patch<T = any, R = T>(url: string, input: T, useCache: boolean = false, options?: HttpOptions): Observable<R> {
        if (useCache && this.cache.has(url)) {
            return of(this.cache.get(url));
        }

        return this.http.patch<R>(url, input, options);
    }

    post<T = any, R = any>(url: string, input: T, useCache: boolean = false, options?: HttpOptions): Observable<R> {
        if (useCache && this.cache.has(url)) {
            return of(this.cache.get(url));
        }

        return this.http.post<R>(url, input, options);
    }

    del<T = any, R = any>(url: string, useCache: boolean = false, options?: HttpOptions): Observable<R> {
        return this.http.delete<R>(url, options);
    }


    private placeInCache<T = any>(url: string, useCache?: boolean): (input: T) => T {
        if (!useCache) {
            return input => input;
        }

        return input => this.cache.set<T>(url, input);
    }

    private extractData(res) {
        return res || res.data;

    }

    private initHeaders(): HttpHeaders {

        const token = this.authService.getToken();

        if (token !== null) {
            const headers = new HttpHeaders({
                'Authorization': 'Bearer ' + token
            });

            return headers;
        }
    }

    private handleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error(`Server returned code ${error.status},` + ` message: ${error.message}`);
        }
        // return an observable with a user-facing error message
        return throwError('Something bad happened; please try again later.');
    }
}
