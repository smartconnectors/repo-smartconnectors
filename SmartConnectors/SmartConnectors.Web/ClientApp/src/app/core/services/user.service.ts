import { Injectable } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { UserProfile } from '../auth/models';
import { Observable, of } from 'rxjs';
import { ORGANIZATION_TYPES } from '../../constants/component.constant';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private authHelper: AuthService = new AuthService();
    private user: UserProfile = null;

    constructor() { }

    public getUserProfile(): Observable<UserProfile> {
        if (this.authHelper.hasValidToken()) {

        }

        return of(this.user);
    }

    public isUserTypeAppPartner() {
        if (ORGANIZATION_TYPES.indexOf(this.user.organizations[0].orgType) >= 0) {
            return false;
        } else {
            return true;
        }
    }

    public hasLastLoginDate() {
        if (this.user && this.user.lastLogin) {
            return true;
        }
        return false;
    }
}
