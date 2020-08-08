import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleOperationDialogComponent } from './schedule-operation-dialog.component';

describe('ScheduleOperationDialogComponent', () => {
  let component: ScheduleOperationDialogComponent;
  let fixture: ComponentFixture<ScheduleOperationDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduleOperationDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduleOperationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
