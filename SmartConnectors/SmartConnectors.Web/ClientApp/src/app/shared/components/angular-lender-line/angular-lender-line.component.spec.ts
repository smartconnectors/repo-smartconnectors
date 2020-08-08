import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AngularLenderLineComponent } from './angular-lender-line.component';

describe('AngularLenderLineComponent', () => {
  let component: AngularLenderLineComponent;
  let fixture: ComponentFixture<AngularLenderLineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AngularLenderLineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AngularLenderLineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
