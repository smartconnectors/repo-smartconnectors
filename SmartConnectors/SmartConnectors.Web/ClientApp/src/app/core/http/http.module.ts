import { NgModule, ModuleWithProviders } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpInterceptorService } from './services/http.interceptor';
import { HttpService } from './services';

@NgModule({
    imports: [
        HttpClientModule
    ],
    declarations: [],
    providers: [{
        provide: HTTP_INTERCEPTORS,
        useClass: HttpInterceptorService,
        multi: true
    }, HttpService]
})
export class HttpModule { }
