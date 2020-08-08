import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpComponent } from './http.component';
import { SharedModule } from '~/shared/shared.module';



@NgModule({
  declarations: [HttpComponent],
  exports: [HttpComponent],
  entryComponents: [HttpComponent],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class HttpConnectorModule { }
