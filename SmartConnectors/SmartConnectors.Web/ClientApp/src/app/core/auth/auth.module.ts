import { NgModule, ModuleWithProviders, Type } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { AUTH_SERVICES } from '~/core/auth/services';

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [],
    providers: []
})

export class AuthModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: AuthModule,
            providers: [AUTH_SERVICES]
        };
    }
}
