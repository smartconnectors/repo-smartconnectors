import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(loginModel: string, workflowId, connectorId) {
        return this.http.post<any>(environment.baseUrl + `account/authenticate`, loginModel)
            .pipe(map(data => {
                // login successful if there's a jwt token in the response
                if (data && data.value) {
                    return data.value.token;
                }
            }));
    }

    refreshToken(workflowId, connectorId) {
        return this.http.get<any>(environment.baseUrl + `/workflows/${workflowId}/${connectorId}/salesforce-refresh-token`)
            .pipe(map(data => {
                // login successful if there's a jwt token in the response
                if (data && data.value) {
                    return data.value.token;
                }
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}