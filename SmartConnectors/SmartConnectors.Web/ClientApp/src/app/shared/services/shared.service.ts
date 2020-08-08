import { Injectable, EventEmitter } from '@angular/core';

interface ISharedService {
  onNavToggle: EventEmitter<any>;
  filterSelected: EventEmitter<any>;
  manageProfileModel: EventEmitter<any>;
  openLoginModel: EventEmitter<any>;
  forgotPasswordModel: EventEmitter<any>;
}

@Injectable()
export class SharedService implements ISharedService {
  public onNavToggle: EventEmitter<any>;
  public filterSelected: EventEmitter<any>;
  public manageProfileModel: EventEmitter<any>;
  public openLoginModel: EventEmitter<any>;
  public forgotPasswordModel: EventEmitter<any>;


  constructor() {
    this.onNavToggle = new EventEmitter();
    this.filterSelected = new EventEmitter();
    this.manageProfileModel = new EventEmitter();
    this.openLoginModel = new EventEmitter();
    this.forgotPasswordModel = new EventEmitter();
  }
}