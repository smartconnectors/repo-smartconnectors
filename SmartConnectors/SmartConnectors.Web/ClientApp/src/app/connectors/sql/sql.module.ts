import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SqlComponent } from './sql.component';
import { SharedModule } from '~/shared/shared.module';



@NgModule({
  declarations: [
    SqlComponent],
  exports: [SqlComponent],
  entryComponents: [SqlComponent],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class SqlModule { }
