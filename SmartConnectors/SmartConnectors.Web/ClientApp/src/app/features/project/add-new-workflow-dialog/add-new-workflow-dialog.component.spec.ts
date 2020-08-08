import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewWorkflowDialogComponent } from "./add-new-workflow-dialog.component";

describe('AddNewWorkflowDialogComponent', () => {
  let component: AddNewWorkflowDialogComponent;
  let fixture: ComponentFixture<AddNewWorkflowDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddNewWorkflowDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewWorkflowDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
