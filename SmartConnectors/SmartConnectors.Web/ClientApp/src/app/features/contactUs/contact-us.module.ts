import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormsModule, ReactiveFormsModule
} from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { ContactUsComponent } from './contact-us.component';
import { SharedModule } from 'src/app/shared/shared.module';

const routes: Routes = [
  {
    path: '',
    component: ContactUsComponent,
    data: {
      breadcrumb: 'Contact Us'
    }
  }
];

@NgModule({
  declarations: [ContactUsComponent],
  entryComponents: [ContactUsComponent],
  exports: [ContactUsComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    RouterModule.forChild(routes)
  ],
  providers: []
})

export class ContactUsModule { }
