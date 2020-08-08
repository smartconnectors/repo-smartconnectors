import { NgModule } from '@angular/core';
import { RouterModule, Route } from '@angular/router';
import { AuthenticationGuard } from './core/auth/guards/authentication.guard';

export const APP_MAIN_ROUTES: Route[] = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        loadChildren: './features/home/home.module#HomeModule'
    }, {
        path: 'project/:id',
        loadChildren: './features/project/project.module#ProjectModule'
    },
    {
        path: 'dashboard',
        loadChildren: './features/dashboard/dashboard.module#DashboardModule',
        canActivate: [AuthenticationGuard]
    },
    {
        path: 'contactus',
        loadChildren: './features/contactUs/contact-us.module#ContactUsModule'
    },
    { path: '**', redirectTo: 'home' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(APP_MAIN_ROUTES, { onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule],
    declarations: []
})
export class AppRoutingModule { }
