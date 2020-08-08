import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '~/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SalesforceService } from './salesforce.service';
import { COMPONENTS } from '.';

@NgModule({
    declarations: [
        COMPONENTS
    ],
    entryComponents: [
        COMPONENTS
    ],
    exports: [
        COMPONENTS
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule
    ],
    providers: [SalesforceService]
})
export class SalesforceModule { }
