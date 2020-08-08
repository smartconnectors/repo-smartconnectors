import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransformationComponent } from './transformation.component';
import { SharedModule } from '~/shared/shared.module';
import { NgJsonEditorModule } from 'ang-jsoneditor'


@NgModule({
  declarations: [TransformationComponent],
  exports: [TransformationComponent],
  entryComponents: [TransformationComponent],
  imports: [
    CommonModule,
    SharedModule,
    NgJsonEditorModule
  ]
})
export class TransformationModule { }
