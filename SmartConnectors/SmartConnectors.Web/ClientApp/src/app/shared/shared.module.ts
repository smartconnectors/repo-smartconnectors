import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SERVICES } from './services';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SHARED_COMPONENTS, MaterialModule } from '.';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { OperationResultComponent } from './components/operation-result/operation-result.component';

@NgModule({
    declarations: [
        SHARED_COMPONENTS,
        OperationResultComponent
    ],
    entryComponents: [
        SHARED_COMPONENTS
    ],
    exports: [
        SHARED_COMPONENTS,
        CommonModule,
        FormsModule,
        RouterModule,
        CommonModule,
        MaterialModule,
        ReactiveFormsModule,
        CKEditorModule,
        FontAwesomeModule,
    ],
    imports: [
        RouterModule,
        CommonModule,
        FormsModule,
        MaterialModule,
        ReactiveFormsModule,
        CKEditorModule
    ],
    providers: [SERVICES]
})

export class SharedModule { }
