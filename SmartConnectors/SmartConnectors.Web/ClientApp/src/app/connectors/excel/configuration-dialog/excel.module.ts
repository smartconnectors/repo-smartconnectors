import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '~/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { COMPONENTS } from '.';
import { ExcelService } from './excel.service';

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
    providers: [ExcelService]
})
export class ExcelModule { }
