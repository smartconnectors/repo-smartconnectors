import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-http',
  templateUrl: './http.component.html',
  styleUrls: ['./http.component.scss']
})
export class HttpComponent implements OnInit {

  @Input() set outputOperationData(value) {
    if (value) {
      this.outputOperations = value;
    } else {
      this.outputOperations = [];
    }
  }

  @Input() set inputOperationData(value) {
    if (value) {
      this.inputOperations = value;
    }
  }

  @Input() set stepData(value) {
    if (value) {
      this.stepCount = value;
    }
  }

  @Output() scriptChanged = new EventEmitter();
  @Output() operationStateChanged = new EventEmitter();

  public data: any = [];
  inputOperations: any[] = [];
  outputOperations: any[] = [];
  requestParams: any[] = [];
  requestHeaders: any[] = [];
  stepCount: number;
  httpForm: FormGroup;
  authorizationTypes: any[] = [{
    id: 1,
    type: 'Bearer Token'
  }];

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.httpForm = this.fb.group({
      path: [],
      method: [],
      authorizationType: [],
      authorizationToken: []
    });
  }
}
