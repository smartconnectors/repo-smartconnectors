/* tslint:disable:no-unused-variable */

import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '~/shared/shared.module';
import { HomeComponent } from '~/features/home/home.component';
import { PaginationModule } from 'ngx-bootstrap';

describe('DashboardComponent', () => {
  let fixture,
    comp;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [HomeComponent],
      imports: [
        CommonModule, FormsModule, PaginationModule.forRoot(),

        RouterTestingModule,

        SharedModule
      ]
    });
    TestBed.compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    comp = fixture.debugElement.componentInstance;

  }));

  beforeEach(() => { });
});
