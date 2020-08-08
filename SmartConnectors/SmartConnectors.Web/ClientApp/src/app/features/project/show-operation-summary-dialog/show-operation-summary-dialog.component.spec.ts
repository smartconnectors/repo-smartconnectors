import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowOperationSummaryDialogComponent } from './show-operation-summary-dialog.component';

describe('ShowOperationSummaryDialogComponent', () => {
  let component: ShowOperationSummaryDialogComponent;
  let fixture: ComponentFixture<ShowOperationSummaryDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowOperationSummaryDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowOperationSummaryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
