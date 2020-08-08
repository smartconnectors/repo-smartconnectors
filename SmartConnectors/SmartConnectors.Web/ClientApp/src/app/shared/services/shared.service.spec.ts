/* tslint:disable:no-unused-variable */

import { TestBed, async } from '@angular/core/testing';
import { SharedService } from './shared.service';

describe('SharedService', () => {
  let service: SharedService;

  beforeEach(() => {
    service = new SharedService();
  });

  it(`should have onNavToggle`, async(() => {  
    expect(service.onNavToggle).toBeDefined();
  }));
});
