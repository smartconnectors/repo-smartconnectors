import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ShellComponent } from './shell.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [ShellComponent],
    entryComponents: [ShellComponent],
    exports: [ShellComponent],
    imports: [
        SharedModule, RouterModule, CommonModule
    ],
    providers: []
})
export class ShellModule { }
