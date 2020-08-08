import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScriptComponent } from './script.component';
import { SharedModule } from '~/shared/shared.module';
import { NgJsonEditorModule } from 'ang-jsoneditor';



@NgModule({
  declarations: [
    ScriptComponent
  ],
  entryComponents: [ScriptComponent],
  exports: [ScriptComponent],
  imports: [
    CommonModule,
    SharedModule,
    NgJsonEditorModule
  ]
})
export class ScriptModule { }
