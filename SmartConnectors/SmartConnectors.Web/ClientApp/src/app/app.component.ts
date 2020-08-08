import { Component, OnInit, OnDestroy } from '@angular/core';
import { LoaderService } from './core/services';
import { Subscription } from 'rxjs';
import { LoaderState } from './core/services/loader.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
    public isAuthenticated: boolean;
    public showLoader = false;
    private _subscriptions: Array<Subscription>;

    constructor(private loaderService: LoaderService) {
        this._subscriptions = new Array<Subscription>();
    }

    ngOnInit(): void {
        this._subscriptions.push(this.loaderService.loaderState
            .subscribe((state: LoaderState) => {
                Promise.resolve().then(() => {
                    this.showLoader = state.show;
                });
            }));
    }

    ngOnDestroy() {
        this._subscriptions.forEach(s => s.unsubscribe());
        this._subscriptions = [];
    }
}
